using UnityEngine;

namespace Core.StateManagement.GameStates
{
    [CreateAssetMenu(fileName = "InitializeTrickState", menuName = "Scripts/GameStates/ScriptableObjects/InitializeTrickState", order = 1)]
    public class InitializeTrickState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName { get; } = "InitializeTrickState";
    }
}