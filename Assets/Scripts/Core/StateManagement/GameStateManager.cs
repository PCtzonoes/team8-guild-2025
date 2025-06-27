using System;
using Core.StateManagement.GameStates;
using UnityEngine;
using Core.StateManagement.States;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

namespace Core.StateManagement
{
    /// <summary>
    /// Simple state manager that manages GameState objects and broadcasts state changes.
    /// Managers can call back to set the next state.
    /// </summary>
    public class GameStateManager : MonoBehaviour
    {
        [Header("Debug")]
        [SerializeField] private bool enableDebugLogs = true;
        
        [SerializeField] private StateChangeEvent stateChangeEvent;

        [SerializeField] private GameState[] chainOfGameStates = new GameState[]
        {
            ScriptableObject.CreateInstance<PreGameNarrativeState>(),
            ScriptableObject.CreateInstance<ShuffleDeckState>(),
            ScriptableObject.CreateInstance<DrawWildCardState>(),
            ScriptableObject.CreateInstance<PlaceBetState>(),
            ScriptableObject.CreateInstance<PlayTrickState>(),
            ScriptableObject.CreateInstance<PreGameNarrativeState>(),
        };
        
        [SerializeField] private string nextScene;
        
        private GameState currentState;
        private int chainIndex = 0;
        
        public GameState CurrentState => currentState;
        
        private void Start()
        {
            NextState();
        }
        
        private void Update()
        {
            // Update current state
            currentState?.OnUpdate();
        }
        
        /// <summary>
        /// Change to a specific state. Can be called by managers.
        /// </summary>
        /// <param name="newState">The state to change to</param>
        public void ChangeToState(GameState newState)
        {
            GameState previousState = currentState;
            
            // Exit current state
            currentState.OnExit();
            
            // Change to new state
            currentState = newState;
            
            // Enter new state
            currentState.OnEnter();
            
            if (enableDebugLogs)
                Debug.Log($"[SimpleStateManager] State changed: {previousState?.StateName ?? "None"} → {newState.StateName}");
        }
        
        /// <summary>
        /// Progress to the next logical state. Can be called by managers.
        /// </summary>
        public void NextState()
        {
            GameState nextState = currentState?.GetNextState()
                                  ?? IncrementGameStateChain();

            if (nextState is null)
            {
                EndScene();
            }
            
            ChangeToState(nextState);
        }
        
        private GameState IncrementGameStateChain()
        {
            chainIndex++;
            return chainIndex <= chainOfGameStates.Length ? chainOfGameStates[chainIndex - 1] : null;
        }

        private void EndScene()
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
