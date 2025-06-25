using System;

namespace DefaultNamespace.Events 
{
    public static class DialogueEvents
    {
        public static event Action<string> OnTriggeredDialogueByName;

        public static void TriggeredDialogueByName(string dialogueName) => OnTriggeredDialogueByName?.Invoke(dialogueName);
    }
}