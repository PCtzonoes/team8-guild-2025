using System;
using Core.StateManagement.ActionStates;
using UnityEngine;
using UnityEngine.Events;

namespace Core.StateManagement
{
    public class StateChangeEvent : ScriptableObject
    {
        // Event for broadcasting state changes
        public UnityAction<GameState, ActionState, Action> OnGameStateChange;

        public void GameStateChanged(GameState gameState, ActionState actionState, Action callback) => 
            OnGameStateChange.Invoke(gameState,  actionState, callback);
    }
}