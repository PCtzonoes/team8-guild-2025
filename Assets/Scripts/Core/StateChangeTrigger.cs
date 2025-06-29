using System;
using System.Collections;
using UnityEngine;
using Core.StateManagement;
using Core.StateManagement.GameStates;
using Core.StateManagement.States;
using GameRound;

namespace Core
{
    /// <summary>
    /// Simple listener that responds to state changes and tells RoundManager what to do.
    /// RoundManager can call back to progress the state.
    /// </summary>
    public class StateChangeTrigger : MonoBehaviour
    {
        [SerializeField] private DialogueEvents dialogueEvents;
        [SerializeField] private GameActionEvent gameActionEvent;
        [SerializeField] private StateChangeEvent stateChangeEvent;
        
        [SerializeField] private RoundManager roundManager;
        
        private bool _playerContinued;
        private bool _completedPlayerAction;
        
        private void Start()
        {
            if (roundManager == null)
                roundManager = FindObjectOfType<RoundManager>();
        }
        
        private void OnEnable()
        {
            stateChangeEvent.OnGameStateChange.AddListener(HandleStateChange);
         }
        
        private void OnDisable()
        {
            stateChangeEvent.OnGameStateChange.RemoveListener(HandleStateChange);
        }
        
        private void HandleStateChange(GameState gameState)
        {
            Debug.Log("State change trigger");
            StartCoroutine(gameState.PerformStateRoutine(roundManager));
        }
    }
}
