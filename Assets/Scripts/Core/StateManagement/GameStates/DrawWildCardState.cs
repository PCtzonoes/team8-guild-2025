using UnityEngine;

namespace Core.StateManagement.States
{
    /// <summary>
    /// State for drawing the wild card (trump suit)
    /// </summary>
    [CreateAssetMenu(fileName = "DrawWildCardState", menuName = "Scripts/GameStates/ScriptableObjects/DrawWildCardState", order = 1)]
    public class DrawWildCardState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName => "draw_wild_card";
    }
}
