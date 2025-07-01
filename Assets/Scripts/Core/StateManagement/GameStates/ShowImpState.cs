using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Core.StateManagement.GameStates
{
    [CreateAssetMenu(fileName = "ShowImpState", menuName = "Scripts/GameStates/ScriptableObjects/ShowImpState", order = 1)]

    public class ShowImpState : GameState
    {
        public int newCardRank;
        public string newCardSuit;

        protected override GameStateManager StateManager { get ; set; }

        public override IEnumerator PerformStateRoutine(RoundManager roundManager)
        {
            _playerContinued = false;

            DeckManager deckManager = FindObjectOfType<DeckManager>();
            Card card = deckManager.DrawCardsFromDeck(1)[0];

            card.cardRank = 0;
            card.cardSuit = "imp";
            card.RenderCard();
            card.transform.SetParent(roundManager.transform);
            card.AnimOnMoveAndRotate(roundManager.WildCardPosition, roundManager.WildCardRotation, 0.1f);
            dialogueEvents.TriggerDialogueByName(StateName);

            yield return WaitForDialogueEnd();
            card.AnimOnMoveAndRotate(roundManager.DiscardPosition, Quaternion.identity, .1f);
            StateManager.NextState();
        }
    }
}