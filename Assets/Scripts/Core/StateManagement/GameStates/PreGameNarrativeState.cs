using System.Collections;
using UnityEngine;

namespace Core.StateManagement.States
{
    /// <summary>
    /// State for pre-game narrative/intro dialogue
    /// </summary>
    [CreateAssetMenu(fileName = "PreGameNarrativeState", menuName = "Scripts/GameStates/ScriptableObjects/PreGameNarrativeState", order = 1)]
    public class PreGameNarrativeState : GameState
    {
        protected override GameStateManager StateManager { get; set; }
        public override string StateName => "pre_game_narrative";
        public override IEnumerator PerformStateRoutine(RoundManager roundManager)
        {
            _playerContinued = false;
            dialogueEvents.TriggerDialogueByName(StateName);
            yield return WaitForDialogueEnd();
            
            StateManager.NextState();
        }
    }
}
