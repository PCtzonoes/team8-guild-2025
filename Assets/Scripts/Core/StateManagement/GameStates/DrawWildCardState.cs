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
        //public new string StateName = "draw_wild_card";
        
        public override IEnumerator PerformStateRoutine(RoundManager roundManager)
        {
            _playerContinued = false;
            dialogueEvents.TriggerDialogueByName(StateName);            
            _completedPlayerAction = false;
            roundManager.SetTrumpCard(Properties);
            yield return WaitForPlayerAction();
            yield return WaitForDialogueEnd();
            StateManager.NextState();
        }
    }
}
