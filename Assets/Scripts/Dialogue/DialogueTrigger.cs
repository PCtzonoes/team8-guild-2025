using DefaultNamespace.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueEvents dialogueEvents;
    [SerializeField] private DialogueManager _dialogueManager;

    
    public Dialogue[] dialoguePhases; // Keep original name to preserve Unity Inspector data
    private Dictionary<string, Dialogue> _dialogueLookup = new();
    
    private void Awake()
    {
        InitializeDialogues();
    }

    private void InitializeDialogues()
    {
        _dialogueLookup.Clear();
        
        foreach (Dialogue dialogue in dialoguePhases)
        {
            _dialogueLookup[dialogue.name] = dialogue;
        }
        
        Debug.Log($"[DialogueTrigger] Initialized {_dialogueLookup.Count} dialogues");
    }

    public void TriggerDialogueByName(string dialogueName)
    {
        Debug.Log($"[DialogueTrigger] Trigger dialogue by {dialogueName}");

        Dialogue lol = dialogueName != null && _dialogueLookup.TryGetValue(dialogueName, out Dialogue dialogue) ? dialogue : null;
        
        _dialogueManager.StartDialogue(lol);
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
