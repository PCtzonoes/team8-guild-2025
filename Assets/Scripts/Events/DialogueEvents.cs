using System;

namespace DefaultNamespace.Events 
{
    public static class DialogueEvents
    {
        public static event Action OnTriggeredDialogue;

        public static event Action<bool> OnContinueDialogue;
        
        public static void TriggeredDialogue() => OnTriggeredDialogue?.Invoke();
        
        public static void ContinueDialogue(bool scrolling) => OnContinueDialogue?.Invoke(scrolling);
    }
}