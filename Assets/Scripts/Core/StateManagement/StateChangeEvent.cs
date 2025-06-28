using System;
using UnityEngine;
using UnityEngine.Events;

namespace Core.StateManagement
{
    [CreateAssetMenu(fileName = "StateChangeEvent", menuName = "Scripts/ScriptableObjects/StateChangeEvent", order = 1)]
    public class StateChangeEvent : ScriptableObject
    {
        // Event for broadcasting state changes
        public UnityEvent<GameState, Action> OnGameStateChange;

        public void GameStateChanged(GameState gameState, Action callback) {
            Debug.Log("[StateChangeEvent] Game state change event");
            OnGameStateChange.Invoke(gameState, callback);
        }
    }
}