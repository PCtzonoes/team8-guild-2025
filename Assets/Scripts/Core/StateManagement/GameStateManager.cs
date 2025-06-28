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

        [SerializeField] private GameState[] chainOfGameStates;
        
        [SerializeField] private string nextScene;

        [SerializeField] GameState currentState;
        
        private int chainIndex = 0;
        
        private void Start()
        {
            NextState();
        }
        
        private void Update()
        {
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
            currentState?.OnExit();
            
            // Change to new state
            currentState = newState;
            
            // Enter new state
            currentState.OnEnter();
            
            if (enableDebugLogs)
                Debug.Log($"[GameStateManager] State changed: {previousState?.StateName ?? "None"} → {newState.StateName}");
            
            // Broadcast state change with callback for progression
            stateChangeEvent.GameStateChanged(newState, () => NextState());
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
                return;
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
            if (enableDebugLogs)
                Debug.Log($"[GameStateManager] End of state chain reached. Loading scene: {nextScene}");
                
            if (!string.IsNullOrEmpty(nextScene))
                SceneManager.LoadScene(nextScene);
        }
    }
}
