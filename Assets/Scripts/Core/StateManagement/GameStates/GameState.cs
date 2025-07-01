using System.Collections;
using GameRound;
using UnityEngine;


namespace Core.StateManagement
{
    /// <summary>
    /// Abstract base class for all game states.
    /// Each state can have its own logic for entering, updating, and exiting.
    /// </summary>
    public abstract class GameState : ScriptableObject
    {
        [SerializeField] protected DialogueEvents dialogueEvents;
        [SerializeField] protected GameActionEvent gameActionEvent;
        
        public static GameStateProperties Properties { get; } = new GameStateProperties();
        
        protected abstract GameStateManager StateManager { get; set; }

        public string StateName;

        protected bool _playerContinued = false;
        protected bool _completedPlayerAction = false;
        
        public virtual void OnEnter(GameStateManager stateManager)
        {
            Debug.Log($"[GameState] Entering: {StateName}");
            StateManager = stateManager;
        }

        public virtual void OnUpdate()
        {
            // Override in derived classes if needed
        }

        public virtual void OnExit()
        {
            Debug.Log($"[GameState] Exiting: {StateName}");
        }

        public virtual GameState GetNextState()
        {
            return null;
        }

        public abstract IEnumerator PerformStateRoutine(RoundManager roundManager);


        protected IEnumerator WaitForDialogueEnd()
        {
            yield return new WaitUntil(() => _playerContinued);
        }

        protected IEnumerator WaitForPlayerAction()
        {
            yield return new WaitUntil(() => _completedPlayerAction);
        }
        
        private void OnDialogueEnd()
        {
            _playerContinued = true;
        }

        private void OnActionEnd()
        {
            _completedPlayerAction = true;
        }
        
        private void OnEnable()
        {
            dialogueEvents = Resources.Load<DialogueEvents>("ScriptableObjects/DialogueEvents");
            gameActionEvent = Resources.Load<GameActionEvent>("ScriptableObjects/GameActionEvent 1");;
            
            dialogueEvents.OnDialogueEnd.AddListener(OnDialogueEnd);
            gameActionEvent.OnActionEnd.AddListener(OnActionEnd);
        }
        
        private void OnDisable()
        {
            dialogueEvents.OnDialogueEnd.RemoveListener(OnDialogueEnd);
            gameActionEvent.OnActionEnd.RemoveListener(OnActionEnd);
        }
    }

    public class GameStateProperties
    {
        public int InitialPlayerHandSize { get; set; }
        
        public int TricksPlayed { get; set; } = 0;

        public int RoundsPlayed { get; set; } = 0;
        
        public string WildCardSuit { get; set; }
        
        public bool IsPlayerLastTrickWinner { get; set; }
        
        public bool IsPlayerRoundWinner { get; set; }
        
        public bool IsRoundEnded { get; set; } = false;
    }
}