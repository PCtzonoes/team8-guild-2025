namespace Core.StateManagement.States
{
    /// <summary>
    /// State for resolving the trick end.
    /// This state requires external logic to determine next state (next trick vs end round).
    /// </summary>
    public class TrickEndState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName => "trick_end";
        
        public override GameState GetNextState()
        {
            // This will be determined by RoundManager callback
            // Default to next trick, but RoundManager will override if round should end
            return new DealOpponentCardsState();
        }
    }
}
