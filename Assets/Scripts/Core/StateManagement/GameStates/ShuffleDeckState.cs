using System.Collections;
using UnityEngine;

namespace Core.StateManagement.States
{
    /// <summary>
    /// State for shuffling the deck
    /// </summary>
    [CreateAssetMenu(fileName = "ShuffleDeckState", menuName = "Scripts/GameStates/ScriptableObjects/ShuffleDeckState", order = 1)]
    public class ShuffleDeckState : GameState
    {
        protected override GameStateManager StateManager { get; set; }
        //public new string StateName = "shuffle_deck";
        public override IEnumerator PerformStateRoutine(RoundManager roundManager)
        {
            Debug.Log("[ShuffleDeckState] DID GET HERE!");
        
            _playerContinued = false;
            dialogueEvents.TriggerDialogueByName(StateName);
            yield return WaitForDialogueEnd();
        
            Debug.Log("[ShuffleDeckState] Executing state action...");
        
            _completedPlayerAction = false;
            roundManager.ShuffleCards(Properties);
            yield return WaitForPlayerAction();
        
            yield return new WaitForSeconds(0.5f);
            
            Debug.Log("[ShuffleDeckState] State routine complete, calling callback");
            StateManager.NextState();
        }
    }
}
