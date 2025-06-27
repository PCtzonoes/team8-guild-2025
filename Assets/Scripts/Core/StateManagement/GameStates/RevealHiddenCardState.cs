namespace Core.StateManagement.States
{
    /// <summary>
    /// State for revealing the opponent's hidden card
    /// </summary>
    public class RevealHiddenCardState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName => "reveal_hidden_card";
        
        public override GameState GetNextState()
        {
            return new TrickEndState();
        }
    }
}
