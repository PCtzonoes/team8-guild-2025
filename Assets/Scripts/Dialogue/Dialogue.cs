using FMODUnity;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue
{
    public string name;
    public bool isBark;
    public bool isFinal;
    public int chance = 100;
    //public EventReference normalSFX;
    [SerializeField] public UnityEvent OnDialogueEnd;
    public DialogueLine[] dialogueLines;
}
