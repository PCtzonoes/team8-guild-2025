namespace Core.StateManagement.States
{
    /// <summary>
    /// State for player placing their bet
    /// </summary>
    public class PlaceBetState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName => "place_bet";
        
        public override GameState GetNextState()
        {
            return new DealOpponentCardsState();
        }
    }
}
