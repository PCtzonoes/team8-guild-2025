using UnityEngine;

namespace Core.StateManagement.States
{
    /// <summary>
    /// Final state - game is complete
    /// </summary>
    [CreateAssetMenu(fileName = "GameEndState", menuName = "Scripts/GameStates/ScriptableObjects/GameEndState", order = 1)]
    public class GameEndState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName => "game_end";
        
        public override GameState GetNextState()
        {
            return null; // Stay in this state
        }
    }
}
