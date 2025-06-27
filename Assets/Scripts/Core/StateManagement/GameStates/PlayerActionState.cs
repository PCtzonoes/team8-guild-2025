namespace Core.StateManagement.States
{
    /// <summary>
    /// State for player playing their card
    /// </summary>
    public class PlayerActionState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName => "player_play_card";
        
        public override GameState GetNextState()
        {
            return new OpponentUsePowerState();
        }
    }
}
