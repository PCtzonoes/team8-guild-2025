using DefaultNamespace.Events;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private float _defaultStartDelay = 0.5f;
    [SerializeField] private float _defaultLetterDelay = 0.15f;

    public Dialogue[] dialoguePhases;

    private void Start()
    {
        for (int i = 0; i < dialoguePhases.Length; i++)
        {
            Dialogue dialogue = dialoguePhases[i];
            for(int j = 0; j < dialogue.dialogueLines.Length; j++)
            {
                if (dialogue.dialogueLines[j].startDelay == 0)
                {
                    dialogue.dialogueLines[j].startDelay = _defaultStartDelay;
                }

                if (dialogue.dialogueLines[j].letterDelay == 0)
                {
                    dialogue.dialogueLines[j].letterDelay = _defaultLetterDelay;
                }
            }
        }
    }

    // load the first dialogue collection
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialoguePhases[0]);
    }

    public void TriggerBark()
    {
        foreach (Dialogue dialogue in dialoguePhases)
        {
            string name = dialogue.name;
            if(name.ToLower() == "barks" || name.ToLower() == "bark")
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                return;
            }
        }
    }

    /// <summary>
    /// event Subscriptions
    /// </summary>
    private void OnEnable()
    {
        DialogueEvents.OnTriggeredDialogue += TriggerDialogue;
    }

    private void OnDisable()
    {
        DialogueEvents.OnTriggeredDialogue -= TriggerDialogue;
    }
}
