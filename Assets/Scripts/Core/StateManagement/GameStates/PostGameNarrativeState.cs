using System.Collections;
using UnityEngine;

namespace Core.StateManagement.States
{
    /// <summary>
    /// State for post-game narrative/outro dialogue
    /// </summary>
    [CreateAssetMenu(fileName = "PostGameNarrativeState", menuName = "Scripts/GameStates/ScriptableObjects/PostGameNarrativeState", order = 1)]
    public class PostGameNarrativeState : GameState
    {
        protected override GameStateManager StateManager { get; set; }
        public override string StateName => "post_game_narrative";
        
        public override IEnumerator PerformStateRoutine(RoundManager roundManager)
        {
            _playerContinued = false;
            dialogueEvents.TriggerDialogueByName(StateName);
            yield return WaitForDialogueEnd();
            
            StateManager.NextState();
        }
    }
}
