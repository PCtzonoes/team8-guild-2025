namespace Core.StateManagement.States
{
    /// <summary>
    /// State for drawing the wild card (trump suit)
    /// </summary>
    public class DrawWildCardState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName => "draw_wild_card";
        
        public override GameState GetNextState()
        {
            return new PlaceBetState();
        }
    }
}
