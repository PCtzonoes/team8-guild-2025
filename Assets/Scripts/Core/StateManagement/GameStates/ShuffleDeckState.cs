namespace Core.StateManagement.States
{
    /// <summary>
    /// State for shuffling the deck
    /// </summary>
    public class ShuffleDeckState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName => "shuffle_deck";
        
        public override GameState GetNextState()
        {
            return new DealPlayerCardsState();
        }
    }
}
