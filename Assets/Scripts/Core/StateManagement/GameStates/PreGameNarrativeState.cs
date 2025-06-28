using UnityEngine;

namespace Core.StateManagement.States
{
    /// <summary>
    /// State for pre-game narrative/intro dialogue
    /// </summary>
    [CreateAssetMenu(fileName = "PreGameNarrativeState", menuName = "Scripts/GameStates/ScriptableObjects/PreGameNarrativeState", order = 1)]
    public class PreGameNarrativeState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName => "pre_game_narrative";
    }
}
