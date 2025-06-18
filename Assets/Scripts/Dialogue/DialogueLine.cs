using FMODUnity;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueLine
{
    public float startDelay = .5f;
    public float letterDelay = .15f;
    [TextArea(3, 10)]
    public string line;
    public UnityEvent OnLineStart;
    //public EventReference sfx;
}
