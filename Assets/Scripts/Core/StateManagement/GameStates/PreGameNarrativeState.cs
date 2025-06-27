namespace Core.StateManagement.States
{
    /// <summary>
    /// State for pre-game narrative/intro dialogue
    /// </summary>
    public class PreGameNarrativeState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName => "pre_game_narrative";
        
        public override GameState GetNextState()
        {
            return new ShuffleDeckState();
        }
    }
}
