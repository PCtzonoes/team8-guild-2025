using System;
using UnityEngine;
using Core.StateManagement;
using Core.StateManagement.ActionStates;
using Core.StateManagement.GameStates;
using Core.StateManagement.States;

namespace Core
{
    /// <summary>
    /// Simple listener that responds to state changes and tells RoundManager what to do.
    /// RoundManager can call back to progress the state.
    /// </summary>
    public class StateChangeTrigger : MonoBehaviour
    {
        [SerializeField] private StateChangeEvent stateChangeEvent;
        
        [SerializeField] private GameStateManager stateManager;
        
        [SerializeField] private RoundManager roundManager;
        
        private void Start()
        {
            // Auto-find references
            if (stateManager == null)
                stateManager = FindObjectOfType<GameStateManager>();
            if (roundManager == null)
                roundManager = FindObjectOfType<RoundManager>();
        }
        
        private void OnEnable()
        {
            stateChangeEvent.OnGameStateChange += HandleStateChange;
        }
        
        private void OnDisable()
        {
            stateChangeEvent.OnGameStateChange -= HandleStateChange;
        }
        
        private void HandleStateChange(GameState gameState, ActionState actionState, Action callback)
        {
            if (actionState is not ActionState.ActionPhase)
            {
                return;
            }
            
            switch (gameState)
            {
                case ShuffleDeckState _:
                    roundManager.ShuffleCards();
                    break;
                    
                case DealPlayerCardsState _:
                    roundManager.DrawPlayerHand(GameState.Properties.InitialPlayerHandSize);
                    break;
                    
                case DrawWildCardState _:
                    roundManager.SetTrumpCard();
                    break;
                    
                case PlaceBetState _:
                    roundManager.StartBetting();
                    break;

                case PlayTrickState _:
                    roundManager.InitializeTrick();
                    break;
                
                // case PlayerActionState _:
                //     roundManager.HandlePlayerPlayCardStep(callback);
                //     break;
                //     
                // case OpponentUsePowerState _:
                //     roundManager.HandleOpponentUsePowerStep(callback);
                //     break;
                //     
                // case RevealHiddenCardState _:
                //     roundManager.HandleRevealHiddenCardStep(callback);
                //     break;
                //     
                // case TrickEndState _:
                //     // RoundManager will decide next state based on game state
                //     roundManager.HandleTrickEndStep(callback);
                //     break;
                    
                case GameEndState _:
                    roundManager.EndGame();
                    break;
            }
        }
        
    }
}
