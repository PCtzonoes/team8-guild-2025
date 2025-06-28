using UnityEngine;
using UnityEngine.Profiling;

namespace Core.StateManagement.GameStates
{
    [CreateAssetMenu(fileName = "InitializeTrickState", menuName = "Scripts/GameStates/ScriptableObjects/InitializeTrickState", order = 1)]
    public class InitializeTrickState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName { get; } = "InitializeTrickState";

        public override GameState GetNextState()
        {
            if (Properties.RoundEnded)
            {
                return null;
            }

            return this;
        }
    }
}