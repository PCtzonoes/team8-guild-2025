using System.Collections;
using UnityEngine;

namespace Core.StateManagement.States
{
    /// <summary>
    /// State for player placing their bet
    /// </summary>
    [CreateAssetMenu(fileName = "PlaceBetState", menuName = "Scripts/GameStates/ScriptableObjects/PlaceBetState", order = 1)]
    public class PlaceBetState : GameState
    {
        protected override GameStateManager StateManager { get; set; }
        public override string StateName => "place_bet";
        public override IEnumerator PerformStateRoutine(RoundManager roundManager)
        {
            Debug.Log("[GameStateManager] DID GET HERE!");
        
            _playerContinued = false;
            dialogueEvents.TriggerDialogueByName(StateName);
            yield return WaitForDialogueEnd();
        
            Debug.Log("[GameStateManager] Executing state action...");
        
            _completedPlayerAction = false;
            roundManager.StartBetting(Properties);
            yield return WaitForPlayerAction();
        
            yield return new WaitForSeconds(0.5f);
            
            Debug.Log("[GameStateManager] State routine complete, calling callback");
            StateManager.NextState();
        }
    }
}
