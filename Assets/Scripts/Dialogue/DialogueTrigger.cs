using DefaultNamespace.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueEvents dialogueEvents;
    
    public Dialogue[] dialoguePhases; // Keep original name to preserve Unity Inspector data
    private Dictionary<string, Dialogue> _dialogueLookup = new Dictionary<string, Dialogue>();
    
    private DialogueManager _dialogueManager;

    private void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        InitializeDialogues();
    }

    private void InitializeDialogues()
    {
        _dialogueLookup.Clear();
        
        if (dialoguePhases == null) return;
        
        foreach (Dialogue dialogue in dialoguePhases)
        {
            _dialogueLookup[dialogue.name] = dialogue;
        }
    }

    public void TriggerDialogueByName(string dialogueName)
    {
        Debug.Log($"[DialogueTrigger] Trigger dialogue by {dialogueName}");
        _dialogueLookup.TryGetValue(dialogueName, out Dialogue dialogue);
        _dialogueManager.StartDialogue(dialogue);
    }

    /// <summary>
    /// event Subscriptions - DialogueTrigger handles all event listening
    /// </summary>
    private void OnEnable()
    {
        dialogueEvents.OnTriggeredDialogueByName.AddListener(TriggerDialogueByName);
    }

    private void OnDisable()
    {
        dialogueEvents.OnTriggeredDialogueByName.RemoveListener(TriggerDialogueByName);
    }
}
