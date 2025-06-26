using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.StateManagement
{
    public class GameStateManager : MonoBehaviour
    {
        [Header("Debug")]
        [SerializeField] private bool enableDebugLogs = true;
        
        private GamePhase currentPhase;
        private IGameState currentStep;
        private Dictionary<GamePhase, List<IGameState>> phaseSteps;
        private int currentStepIndex;
        private List<ITriggerHandler> triggerHandlers;
        
        public GamePhase CurrentPhase => currentPhase;
        public IGameState CurrentStep => currentStep;
        public string CurrentStepName => currentStep?.StateName ?? "None";
        
        public event Action<GamePhase, GamePhase> OnPhaseChanged;
        public event Action<IGameState, IGameState> OnStepChanged;
        
        private void Awake()
        {
            InitializeStateManager();
        }
        
        private void Start()
        {
            StartGame();
        }
        
        private void Update()
        {
            UpdateCurrentStep();
        }
        
        private void InitializeStateManager()
        {
            phaseSteps = new Dictionary<GamePhase, List<IGameState>>();
            triggerHandlers = new List<ITriggerHandler>();
            
            // Initialize phase step collections
            phaseSteps[GamePhase.PreGame] = new List<IGameState>();
            phaseSteps[GamePhase.Setup] = new List<IGameState>();
            phaseSteps[GamePhase.Play] = new List<IGameState>();
            phaseSteps[GamePhase.PostGame] = new List<IGameState>();
            
            SetupPhaseSteps();
        }
        
        private void SetupPhaseSteps()
        {
            // PreGame Phase Steps
            phaseSteps[GamePhase.PreGame].Add(new NarrativeStep("PreGame Narrative"));
            
            // Setup Phase Steps
            phaseSteps[GamePhase.Setup].Add(new ShuffleDeckStep());
            phaseSteps[GamePhase.Setup].Add(new DealPlayerCardsStep());
            phaseSteps[GamePhase.Setup].Add(new DrawWildCardStep());
            phaseSteps[GamePhase.Setup].Add(new PlaceBetStep());
            
            // Play Phase Steps
            phaseSteps[GamePhase.Play].Add(new DealOpponentCardsStep());
            phaseSteps[GamePhase.Play].Add(new PlayerUsePowerStep());
            phaseSteps[GamePhase.Play].Add(new PlayerPlayCardStep());
            phaseSteps[GamePhase.Play].Add(new OpponentUsePowerStep());
            phaseSteps[GamePhase.Play].Add(new RevealHiddenCardStep());
            phaseSteps[GamePhase.Play].Add(new TrickEndStep());
            phaseSteps[GamePhase.Play].Add(new GameEndStep());
            
            // PostGame Phase Steps
            phaseSteps[GamePhase.PostGame].Add(new NarrativeStep("PostGame Narrative"));
        }
        
        public void RegisterTriggerHandler(ITriggerHandler handler)
        {
            if (!triggerHandlers.Contains(handler))
            {
                triggerHandlers.Add(handler);
                DebugLog($"Registered trigger handler: {handler.GetType().Name}");
            }
        }
        
        public void UnregisterTriggerHandler(ITriggerHandler handler)
        {
            if (triggerHandlers.Contains(handler))
            {
                triggerHandlers.Remove(handler);
                DebugLog($"Unregistered trigger handler: {handler.GetType().Name}");
            }
        }
        
        private void StartGame()
        {
            ChangePhase(GamePhase.PreGame);
        }
        
        private void ChangePhase(GamePhase newPhase)
        {
            var previousPhase = currentPhase;
            
            // Exit current step if exists
            currentStep?.Exit();
            
            currentPhase = newPhase;
            currentStepIndex = 0;
            
            DebugLog($"Phase changed: {previousPhase} -> {newPhase}");
            
            // Notify trigger handlers about phase change
            foreach (var handler in triggerHandlers)
            {
                handler.HandlePhaseChange(newPhase, previousPhase);
            }
            
            OnPhaseChanged?.Invoke(newPhase, previousPhase);
            
            // Start first step of new phase
            if (phaseSteps[currentPhase].Count > 0)
            {
                ChangeStep(phaseSteps[currentPhase][0]);
            }
        }
        
        private void ChangeStep(IGameState newStep)
        {
            var previousStep = currentStep;
            
            // Exit current step
            currentStep?.Exit();
            
            currentStep = newStep;
            
            DebugLog($"Step changed: {previousStep?.StateName ?? "None"} -> {newStep.StateName}");
            
            // Notify trigger handlers about step change
            foreach (var handler in triggerHandlers)
            {
                handler.HandleStepChange(newStep, previousStep);
            }
            
            OnStepChanged?.Invoke(newStep, previousStep);
            
            // Enter new step
            currentStep.Enter();
        }
        
        private void UpdateCurrentStep()
        {
            if (currentStep == null) return;
            
            currentStep.Update();
            
            // Check if current step can transition
            if (currentStep.CanTransition())
            {
                NextStep();
            }
        }
        
        public void NextStep()
        {
            var currentPhaseSteps = phaseSteps[currentPhase];
            currentStepIndex++;
            
            // Check if we've completed all steps in current phase
            if (currentStepIndex >= currentPhaseSteps.Count)
            {
                NextPhase();
                return;
            }
            
            // Move to next step in current phase
            ChangeStep(currentPhaseSteps[currentStepIndex]);
        }
        
        private void NextPhase()
        {
            switch (currentPhase)
            {
                case GamePhase.PreGame:
                    ChangePhase(GamePhase.Setup);
                    break;
                case GamePhase.Setup:
                    ChangePhase(GamePhase.Play);
                    break;
                case GamePhase.Play:
                    ChangePhase(GamePhase.PostGame);
                    break;
                case GamePhase.PostGame:
                    // Game complete - could restart or return to menu
                    DebugLog("Game Complete!");
                    break;
            }
        }
        
        public void ForceNextStep()
        {
            DebugLog("Forcing next step transition");
            NextStep();
        }
        
        public void ForceNextPhase()
        {
            DebugLog("Forcing next phase transition");
            NextPhase();
        }
        
        private void DebugLog(string message)
        {
            if (enableDebugLogs)
            {
                Debug.Log($"[GameStateManager] {message}");
            }
        }
        
        // Public methods for external systems to query state
        public bool IsInPhase(GamePhase phase) => currentPhase == phase;
        public bool IsInStep<T>() where T : IGameState => currentStep is T;
        public T GetCurrentStepAs<T>() where T : class, IGameState => currentStep as T;
    }
}
