using System.Collections.Generic;
using UnityEngine;

namespace Core.StateManagement
{
    /// <summary>
    /// Abstract base class for all game states.
    /// Each state can have its own logic for entering, updating, and exiting.
    /// </summary>
    public abstract class GameState : ScriptableObject
    {
        public static GameStateProperties Properties { get; } = new GameStateProperties();
        
        protected abstract GameStateManager StateManager { get; }
        
        /// <summary>
        /// Unique name for this state
        /// </summary>
        public abstract string StateName { get; }
        
        /// <summary>
        /// Called when entering this state
        /// </summary>c
        public virtual void OnEnter()
        {
            Debug.Log($"[GameState] Entering: {StateName}");
        }

        /// <summary>
        /// Called every frame while in this state
        /// </summary>
        public virtual void OnUpdate()
        {
            // Override in derived classes if needed
        }

        /// <summary>
        /// Called when exiting this state
        /// </summary>
        public virtual void OnExit()
        {
            Debug.Log($"[GameState] Exiting: {StateName}");
        }

        /// <summary>
        /// Get the next logical state. Override in derived classes for custom logic.
        /// </summary>
        /// <returns>The next state to transition to, or null to stay in current state</returns>
        public virtual GameState GetNextState()
        {
            return null; // Default: no automatic progression
        }

        public virtual string GetPreActionDialogueName()
        {
            return StateName;
        }

        public virtual string GetPostActionDialogueName()
        {
            return null;
        }
    }

    public class GameStateProperties
    {
        public int InitialPlayerHandSize { get; private set; } = 5;
        
        public int TricksPlayed { get; set; } = 0;
        public string WildCardSuit { get; set; }

        public bool RoundEnded { get; set; } = false;
    }
}
