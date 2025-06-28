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
                case PlayTrickState _:
                    stateAction = roundManager.PlayTrick;
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
            dialogueEvents.TriggerDialogueByName(preActionDialogueName);

            yield return WaitForPlayerContinue();
            
            Debug.Log("[StateChangeTrigger] DID GET HERE!");
            
            stateAction(GameState.Properties);
            
            yield return WaitForPlayerContinue();
            
            string postActionDialogueName = state.GetPostActionDialogueName();
            dialogueEvents.TriggerDialogueByName(postActionDialogueName);

            yield return WaitForPlayerContinue();

            callback();
        }
        
        private IEnumerator WaitForPlayerContinue()
        {
            _playerContinued = false;
            yield return new WaitUntil(() => _playerContinued);
        }
        
        private void OnDialogueEnd()
        {
            _playerContinued = true;
        }

        private void OnActionEnd()
        {
            _playerContinued = true;
        }
    }
}
