using System;
using System.Collections;
using System.Collections.Generic;
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
            dialogueEvents.OnDialogueEnd.AddListener(OnDialogueEnd);
            gameActionEvent.OnActionEnd.AddListener(OnActionEnd);
         }
        
        private void OnDisable()
        {
            stateChangeEvent.OnGameStateChange.RemoveListener(HandleStateChange);
            dialogueEvents.OnDialogueEnd.RemoveListener(OnDialogueEnd);
            gameActionEvent.OnActionEnd.RemoveListener(OnActionEnd);
        }
        
        private void HandleStateChange(GameState gameState, Action callback)
        {
            Debug.Log("State change trigger");
            
            Action<GameStateProperties> stateAction;
            
            switch (gameState)
            {
                case ShuffleDeckState _:
                    stateAction = roundManager.ShuffleCards;
                    break;
                case DealPlayerCardsState _:
                    stateAction = roundManager.DrawPlayerHand;
                    break;
                case DrawWildCardState _:
                    stateAction = roundManager.SetTrumpCard;
                    break;
                case PlaceBetState _:
                    stateAction = roundManager.StartBetting;
                    break;
                case InitializeTrickState _:
                    stateAction = roundManager.InitializeTrick;
                    break;
                case GameEndState _:
                    stateAction = roundManager.EndGame;
                    break;
                default:
                    return;
            }

            StartCoroutine(PerformStateRoutine(gameState, stateAction, callback));
        }

        private IEnumerator PerformStateRoutine(
            GameState state, 
            Action<GameStateProperties> stateAction,
            Action callback)
        {
            Debug.Log("[StateChangeTrigger] DID GET HERE!");
            
            string preActionDialogueName = state.GetPreActionDialogueName();
        
            Debug.Log($"[StateChangeTrigger] Triggering pre-action dialogue: {preActionDialogueName}");
            _playerContinued = false;
            dialogueEvents.TriggerDialogueByName(preActionDialogueName);
            yield return WaitForPlayerContinue();
            
            Debug.Log("[StateChangeTrigger] Executing state action...");
            
            _completedPlayerAction = false;
            stateAction(GameState.Properties);
            yield return WaitForPlayerAction();
            
            string postActionDialogueName = state.GetPostActionDialogueName();
            
            yield return new WaitForSeconds(0.5f);
            
            Debug.Log($"[StateChangeTrigger] Triggering post-action dialogue: {postActionDialogueName}");
            _playerContinued = false;
            dialogueEvents.TriggerDialogueByName(postActionDialogueName);
            yield return WaitForPlayerContinue();

            Debug.Log("[StateChangeTrigger] State routine complete, calling callback");
            callback();
        }
        
        private IEnumerator WaitForPlayerContinue()
        {
            Debug.Log($"[StateChangeTrigger] Waiting for player continue... {_playerContinued}");
            yield return new WaitUntil(() => _playerContinued);
        }

        private IEnumerator WaitForPlayerAction()
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
    }
}
