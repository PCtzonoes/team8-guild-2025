using System;

namespace DefaultNamespace.Events 
{
    public static class DialogueEvents
    {
        public static event Action OnTriggeredDialogue;

        public static void TriggeredDialogue() => OnTriggeredDialogue?.Invoke();
    }
}