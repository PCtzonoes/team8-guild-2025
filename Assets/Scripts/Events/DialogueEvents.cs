using System;

namespace DefaultNamespace.Events 
{
    public static class DialogueEvents
    {
        public static event Action OnTriggeredDialogue;

        public static event Action OnWinDialogue;

        public static event Action OnLoseDialogue;

        public static void TriggeredDialogue() => OnTriggeredDialogue?.Invoke();
        public static void WinDialogue() => OnWinDialogue?.Invoke();
        public static void LoseDialogue() => OnLoseDialogue?.Invoke();

    }
}