using Core.StateManagement.States;
using UnityEngine;

namespace Core.StateManagement.GameStates
{
    [CreateAssetMenu(fileName = "PlayTrickState", menuName = "Scripts/GameStates/ScriptableObjects/PlayTrickState", order = 1)]
    public class PlayTrickState : GameState
    {
        protected override GameStateManager StateManager { get; }
        public override string StateName { get; } = "play";
    }
}