using FMODUnity;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue
{
    public string name;
    [SerializeField] public UnityEvent OnDialogueEnd;
    public DialogueLine[] dialogueLines;
    
    public void EndDialogue() => OnDialogueEnd?.Invoke();
}
