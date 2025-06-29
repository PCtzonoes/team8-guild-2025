using System.Collections;
using UnityEngine;

namespace Core.StateManagement.States
{
    /// <summary>
    /// Final state - game is complete
    /// </summary>
    [CreateAssetMenu(fileName = "GameEndState", menuName = "Scripts/GameStates/ScriptableObjects/GameEndState", order = 1)]
    public class GameEndState : GameState
    {
        protected override GameStateManager StateManager { get; set; }
        //public new string StateName = "game_end";

        public override IEnumerator PerformStateRoutine(RoundManager roundManager)
        {
            yield return new WaitForSeconds(0.2f);
            StateManager.NextState();
        }
    }
}
