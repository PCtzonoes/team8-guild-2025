namespace Core.StateManagement.States
{
    /// <summary>
    /// State for post-game narrative/outro dialogue
    /// </summary>
    public class PostGameNarrativeState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName => "post_game_narrative";
        
        public override GameState GetNextState()
        {
            return new GameEndState();
        }
    }
}
