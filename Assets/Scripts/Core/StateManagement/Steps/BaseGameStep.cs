using UnityEngine;

namespace Core.StateManagement
{
    public abstract class BaseGameStep : IGameState
    {
        protected bool isComplete;
        protected float stepTimer;
        
        public abstract string StateName { get; }
        
        public virtual void Enter()
        {
            isComplete = false;
            stepTimer = 0f;
            OnEnter();
        }
        
        public virtual void Update()
        {
            stepTimer += Time.deltaTime;
            OnUpdate();
        }
        
        public virtual void Exit()
        {
            OnExit();
        }
        
        public virtual bool CanTransition()
        {
            return isComplete;
        }
        
        protected void CompleteStep()
        {
            isComplete = true;
        }
        
        // Override these in derived classes
        protected virtual void OnEnter() { }
        protected virtual void OnUpdate() { }
        protected virtual void OnExit() { }
    }
}
