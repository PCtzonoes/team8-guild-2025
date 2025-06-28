using UnityEngine;

namespace Core.StateManagement.States
{
    /// <summary>
    /// State for player placing their bet
    /// </summary>
    [CreateAssetMenu(fileName = "PlaceBetState", menuName = "Scripts/GameStates/ScriptableObjects/PlaceBetState", order = 1)]
    public class PlaceBetState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName => "place_bet";
    }
}
