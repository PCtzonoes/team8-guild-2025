using System.Collections;

namespace Core.StateManagement.GameStates
{
    public class TutorialFirstRoundSetupState : GameState
    {
        protected override GameStateManager StateManager { get; set; }
        public override string StateName { get; }
        public override IEnumerator PerformStateRoutine(RoundManager roundManager)
        {
            throw new System.NotImplementedException();
        }
    }
}