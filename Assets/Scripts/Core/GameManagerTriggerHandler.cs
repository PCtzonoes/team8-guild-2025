using UnityEngine;
using Core.StateManagement;

namespace Core
{
    public class GameManagerTriggerHandler : MonoBehaviour, ITriggerHandler
    {
        [Header("References")]
        [SerializeField] private GameStateManager stateManager;
        
        // Add references to your game systems here
        // [SerializeField] private DeckManager deckManager;
        // [SerializeField] private CardManager cardManager;
        // [SerializeField] private BettingManager bettingManager;
        
        private void Start()
        {
            if (stateManager == null)
                stateManager = FindObjectOfType<GameStateManager>();
                
            stateManager.RegisterTriggerHandler(this);
        }
        
        private void OnDestroy()
        {
            if (stateManager != null)
                stateManager.UnregisterTriggerHandler(this);
        }
        
        public void HandlePhaseChange(GamePhase newPhase, GamePhase previousPhase)
        {
            Debug.Log($"[GameManager] Phase changed: {previousPhase} -> {newPhase}");
            
            switch (newPhase)
            {
                case GamePhase.PreGame:
                    HandlePreGamePhase();
                    break;
                case GamePhase.Setup:
                    HandleSetupPhase();
                    break;
                case GamePhase.Play:
                    HandlePlayPhase();
                    break;
                case GamePhase.PostGame:
                    HandlePostGamePhase();
                    break;
            }
        }
        
        public void HandleStepChange(IGameState newStep, IGameState previousStep)
        {
            Debug.Log($"[GameManager] Step changed: {previousStep?.StateName ?? "None"} -> {newStep.StateName}");
            
            // Handle specific step transitions
            switch (newStep)
            {
                case ShuffleDeckStep shuffleStep:
                    HandleShuffleDeck();
                    break;
                case DealPlayerCardsStep dealStep:
                    HandleDealPlayerCards();
                    break;
                case DrawWildCardStep wildStep:
                    HandleDrawWildCard();
                    break;
                case PlaceBetStep betStep:
                    HandlePlaceBet();
                    break;
                case DealOpponentCardsStep opponentDealStep:
                    HandleDealOpponentCards();
                    break;
                case PlayerUsePowerStep playerPowerStep:
                    HandlePlayerUsePower();
                    break;
                case PlayerPlayCardStep playerCardStep:
                    HandlePlayerPlayCard();
                    break;
                case OpponentUsePowerStep opponentPowerStep:
                    HandleOpponentUsePower();
                    break;
                case RevealHiddenCardStep revealStep:
                    HandleRevealHiddenCard();
                    break;
                case TrickEndStep trickEndStep:
                    HandleTrickEnd();
                    break;
                case GameEndStep gameEndStep:
                    HandleGameEnd();
                    break;
            }
        }
        
        private void HandlePreGamePhase()
        {
            // Initialize game state, reset scores, etc.
        }
        
        private void HandleSetupPhase()
        {
            // Prepare for new round
        }
        
        private void HandlePlayPhase()
        {
            // Enable gameplay systems
        }
        
        private void HandlePostGamePhase()
        {
            // Calculate final scores, show results
        }
        
        private void HandleShuffleDeck()
        {
            // deckManager.ShuffleDeck();
            // Animation or immediate completion
        }
        
        private void HandleDealPlayerCards()
        {
            // cardManager.DealCardsToPlayer();
        }
        
        private void HandleDrawWildCard()
        {
            // cardManager.DrawWildCard();
        }
        
        private void HandlePlaceBet()
        {
            // bettingManager.EnableBetting();
            // UI will call the step's OnBetPlaced() when player bets
        }
        
        private void HandleDealOpponentCards()
        {
            // cardManager.DealCardsToOpponent();
        }
        
        private void HandlePlayerUsePower()
        {
            // Enable power usage UI
            // UI will call the step's OnPowerUsed() or OnPowerSkipped()
        }
        
        private void HandlePlayerPlayCard()
        {
            // Enable card selection
            // UI will call the step's OnCardPlayed()
        }
        
        private void HandleOpponentUsePower()
        {
            // AI logic for power usage
        }
        
        private void HandleRevealHiddenCard()
        {
            // Reveal opponent's played card
        }
        
        private void HandleTrickEnd()
        {
            // Calculate trick winner, update scores
        }
        
        private void HandleGameEnd()
        {
            // Check win conditions, decide if game continues
        }
    }
}
