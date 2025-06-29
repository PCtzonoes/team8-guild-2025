using System.Collections;
using UnityEngine;

namespace Core.StateManagement.States
{
    /// <summary>
    /// State for dealing cards to the player
    /// </summary>
    [CreateAssetMenu(fileName = "DealPlayerCardsState", menuName = "Scripts/GameStates/ScriptableObjects/DealPlayerCardsState", order = 1)]
    public class DealPlayerCardsState : GameState
    {
        protected override GameStateManager StateManager { get; set; }
        //public new string StateName = "deal_player_cards";

        public override IEnumerator PerformStateRoutine(RoundManager roundManager)
        {
            Debug.Log("[DealPlayerCardsState] Executing state action...");
        
            _playerContinued = false;
            dialogueEvents.TriggerDialogueByName(StateName);
            
            _completedPlayerAction = false;
            roundManager.DrawPlayerHand(Properties);
            yield return WaitForPlayerAction();
            yield return WaitForDialogueEnd();
            yield return new WaitForSeconds(0.5f);
            
            Debug.Log("[DealPlayerCardsState] State routine complete, calling callback");
            StateManager.NextState();
        }
    }
}
