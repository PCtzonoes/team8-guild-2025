using UnityEngine;

namespace Core.StateManagement.States
{
    /// <summary>
    /// State for dealing cards to the opponent (start of trick)
    /// </summary>
    public class DealOpponentCardsState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName => "deal_opponent_cards";
        
        public override GameState GetNextState()
        {
            return CreateInstance<PlayerActionState>();
        }
    }
}
