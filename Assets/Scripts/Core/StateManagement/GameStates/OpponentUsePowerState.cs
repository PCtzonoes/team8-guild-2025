namespace Core.StateManagement.States
{
    /// <summary>
    /// State for opponent using their power (optional)
    /// </summary>
    public class OpponentUsePowerState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName => "opponent_use_power";
        
        public override GameState GetNextState()
        {
            return new RevealHiddenCardState();
        }
    }
}
