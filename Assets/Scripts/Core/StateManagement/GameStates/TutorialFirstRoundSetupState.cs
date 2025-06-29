using System.Collections;

namespace Core.StateManagement.GameStates
{
    public class TutorialFirstRoundSetupState : GameState
    {
        protected override GameStateManager StateManager { get; set; }
        //public new string StateName = "tutorial_first_round_setup";
        public override IEnumerator PerformStateRoutine(RoundManager roundManager)
        {
            throw new System.NotImplementedException();
        }
    }
}