using System;

namespace DefaultNamespace.Events 
{
    public static class DialogueEvents
    {
        // Updated to accept Dialogue parameter for consistency
        public static event Action<Dialogue> OnTriggeredDialogue;

        public static void TriggeredDialogue(Dialogue dialogue) => OnTriggeredDialogue?.Invoke(dialogue);
        
        // Keep the parameterless version for backward compatibility if needed
        public static void TriggeredDialogue() => OnTriggeredDialogue?.Invoke(null);
    }
}