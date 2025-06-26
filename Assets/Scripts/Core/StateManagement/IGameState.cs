using UnityEngine;

namespace Core.StateManagement
{
    public interface IGameState
    {
        void Enter();
        void Update();
        void Exit();
        bool CanTransition();
        string StateName { get; }
    }
}
