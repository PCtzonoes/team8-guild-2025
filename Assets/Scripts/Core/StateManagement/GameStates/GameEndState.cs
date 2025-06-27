namespace Core.StateManagement.States
{
    /// <summary>
    /// Final state - game is complete
    /// </summary>
    public class GameEndState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName => "game_end";
        
        public override GameState GetNextState()
        {
            return null; // Stay in this state
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            UnityEngine.Debug.Log("[GameEndState] Game Complete!");
        }
    }
}
