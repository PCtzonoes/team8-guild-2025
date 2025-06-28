using UnityEngine;

namespace Core.StateManagement.States
{
    /// <summary>
    /// State for dealing cards to the player
    /// </summary>
    [CreateAssetMenu(fileName = "DealPlayerCardsState", menuName = "Scripts/GameStates/ScriptableObjects/DealPlayerCardsState", order = 1)]
    public class DealPlayerCardsState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName => "deal_player_cards";
        
        public override GameState GetNextState()
        {
            return new DrawWildCardState();
        }
    }
}
