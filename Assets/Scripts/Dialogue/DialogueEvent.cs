using UnityEngine;
using UnityEngine.Events;

public class DialogueEvent : ScriptableObject
{
    public UnityAction<string> OnTriggeredDialogueByName;
    
    public void TriggerDialogueByName(string dialogueName) => OnTriggeredDialogueByName.Invoke(dialogueName);
}
