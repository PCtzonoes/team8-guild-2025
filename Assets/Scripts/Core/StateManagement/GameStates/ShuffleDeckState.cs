using UnityEngine;

namespace Core.StateManagement.States
{
    /// <summary>
    /// State for shuffling the deck
    /// </summary>
    [CreateAssetMenu(fileName = "ShuffleDeckState", menuName = "Scripts/GameStates/ScriptableObjects/ShuffleDeckState", order = 1)]
    public class ShuffleDeckState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName => "shuffle_deck";
    }
}
