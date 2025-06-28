using UnityEngine;
using UnityEngine.Events;

namespace GameRound
{
    [CreateAssetMenu(fileName = "GameActionEvent", menuName = "Scripts/ScriptableObjects/GameActionEvent", order = 1)]
    public class GameActionEvent : ScriptableObject
    {
        public UnityEvent OnActionEnd;
    
        public void ActionEnd() => OnActionEnd.Invoke();
    }
}