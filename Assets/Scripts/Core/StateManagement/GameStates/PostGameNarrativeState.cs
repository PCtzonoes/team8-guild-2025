using UnityEngine;

namespace Core.StateManagement.States
{
    /// <summary>
    /// State for post-game narrative/outro dialogue
    /// </summary>
    [CreateAssetMenu(fileName = "PostGameNarrativeState", menuName = "Scripts/GameStates/ScriptableObjects/PostGameNarrativeState", order = 1)]
    public class PostGameNarrativeState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName => "post_game_narrative";
    }
}
