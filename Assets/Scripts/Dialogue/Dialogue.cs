using FMODUnity;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue
{
    public string name;
    //public EventReference normalSFX;
    [SerializeField] public UnityEvent OnDialogueEnd;
    public DialogueLine[] dialogueLines;
}
