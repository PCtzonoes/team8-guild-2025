using System.Collections;
using UnityEngine;

namespace Core.StateManagement.States
{
    /// <summary>
    /// State for drawing the wild card (trump suit)
    /// </summary>
    [CreateAssetMenu(fileName = "DrawWildCardState", menuName = "Scripts/GameStates/ScriptableObjects/DrawWildCardState", order = 1)]
    public class DrawWildCardState : GameState
    {
        protected override GameStateManager StateManager { get; set; }
        public override string StateName => "draw_wild_card";
        
        public override IEnumerator PerformStateRoutine(RoundManager roundManager)
        {
            _playerContinued = false;
            dialogueEvents.TriggerDialogueByName(StateName);
            yield return WaitForDialogueEnd();
            
            _completedPlayerAction = false;
            roundManager.DrawPlayerHand(Properties);
            yield return WaitForPlayerAction();
            
            StateManager.NextState();
        }
    }
}
