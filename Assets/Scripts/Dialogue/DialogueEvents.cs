using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DialogueEvents", menuName = "Scripts/ScriptableObjects/DialogueEvents", order = 1)]

public class DialogueEvents : ScriptableObject
{
    public UnityEvent<string> OnTriggeredDialogueByName;
    
    public UnityEvent OnDialogueEnd;
    
    public void TriggerDialogueByName(string dialogueName) =>
        OnTriggeredDialogueByName.Invoke(dialogueName);

    public void DialogueEnd() => OnDialogueEnd.Invoke();
}
