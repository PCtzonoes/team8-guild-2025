using Core.StateManagement.States;

namespace Core.StateManagement.GameStates
{
    public class PlayTrickState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName { get; } = "play";

        public override GameState GetNextState()
        {
            return CreateInstance<DealOpponentCardsState>();
        }
    }
}