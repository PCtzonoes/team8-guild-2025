using DefaultNamespace.Events;
using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Core.StateManagement.States
{
    /// <summary>
    /// State for drawing the wild card (trump suit)
    /// </summary>
    [CreateAssetMenu(fileName = "DrawWildCardState", menuName = "Scripts/GameStates/ScriptableObjects/DrawWildCardState", order = 1)]
    public class DrawWildCardState : GameState
    {
        protected override GameStateManager StateManager { get; set; }
        //public new string StateName = "draw_wild_card";
        
        public override IEnumerator PerformStateRoutine(RoundManager roundManager)
        {
            _playerContinued = false;
            dialogueEvents.TriggerDialogueByName(StateName + "_" + Properties.RoundsPlayed);            
            _completedPlayerAction = false;
            //roundManager.SetTrumpCard(Properties);
            
            Card wildCard = roundManager.deckManager.DrawCardsFromDeck(1)[0];
            if (wildCard.cardSuit != "hearts" && wildCard.cardSuit != "clubs" && wildCard.cardSuit != "diamonds" && wildCard.cardSuit != "spades")
            {
                wildCard.cardSuit = "spades";
                wildCard.cardRank = 14;
                wildCard.RenderCard();
            }

            roundManager.wildCardSuit = wildCard.cardSuit;

            wildCard.transform.SetParent(roundManager.transform);
            wildCard.AnimOnMoveAndRotate(roundManager.WildCardPosition, roundManager.WildCardRotation, 0.1f);

            GameEvents.SetWildCardSuit(roundManager.wildCardSuit);
            yield return WaitForDialogueEnd();
            yield return new WaitForSeconds(1f);
            wildCard.AnimOnMoveAndRotate(roundManager.DiscardPosition, Quaternion.identity, 0f);
            StateManager.NextState();
        }
    }
}
