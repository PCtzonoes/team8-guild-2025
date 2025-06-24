using DefaultNamespace.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private float _defaultStartDelay = 0.5f;
    [SerializeField] private float _defaultLetterDelay = 0.15f;
    [SerializeField] private bool _startOnLoad = false;

    public Dialogue[] dialoguePhases;

    public int _currentPhase = 1;
    public int currentBark = 1;
    
    // Memory leak fixes - cache references and reuse collections
    private DialogueManager _dialogueManager;
    private List<Dialogue> _tempBarks = new List<Dialogue>();
    private List<Dialogue> _tempWinningBarks = new List<Dialogue>();
    private List<Dialogue> _tempLosingBarks = new List<Dialogue>();

    private void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        
        // set default delays
        for (int i = 0; i < dialoguePhases.Length; i++)
        {
            Dialogue dialogue = dialoguePhases[i];

            for (int j = 0; j < dialogue.dialogueLines.Length; j++)
            {
                if (dialogue.dialogueLines[j] == null) continue;
                
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
        
        if (_startOnLoad)
        {
            StartCoroutine(SlowTrigger());
        }
    }

    // Updated to handle both parameterless and parameterized dialogue triggering
    public void TriggerDialogue(Dialogue dialogue = null)
    {
        // If a specific dialogue is provided, use it
        if (dialogue != null)
        {
            _dialogueManager.StartDialogue(dialogue);
            return;
        }
        
        if (dialoguePhases == null || dialoguePhases.Length == 0)
        {
            return;
        }
        
        // Bounds checking
        while (_currentPhase <= dialoguePhases.Length && dialoguePhases[_currentPhase - 1] != null && dialoguePhases[_currentPhase - 1].isBark == true)
        {
            _currentPhase++;
        }
        
        // Final bounds check
        if (_currentPhase > dialoguePhases.Length)
        {
            Debug.LogWarning("Attempted to trigger dialogue beyond available phases!");
            _currentPhase = dialoguePhases.Length;
            return;
        }
        
        _dialogueManager.StartDialogue(dialoguePhases[_currentPhase-1]);
        _currentPhase++;
        
        if(_currentPhase > dialoguePhases.Length)
        {
            _currentPhase--;
        }
    }

    // Remove the separate TriggerSpecificDialogue method since it's now handled by TriggerDialogue

    public void TriggerBark(bool didPlayerWin)
    {
        if (_dialogueManager == null || dialoguePhases == null || dialoguePhases.Length == 0)
        {
            return;
        }
        
        // Clear and reuse existing lists to prevent memory allocations
        _tempBarks.Clear();
        _tempWinningBarks.Clear();
        _tempLosingBarks.Clear();

        // find all dialogues that are the current bark
        foreach (Dialogue dialogue in dialoguePhases)
        {
            if (dialogue == null || !dialogue.isBark) continue;
            
            if (dialogue.name != null && dialogue.name.Contains(currentBark.ToString()))
            {
                _tempBarks.Add(dialogue);
            }
        }

        // if there isn't a bark for this trick, don't call it
        bool foundTrickBark = false;
        for(int i = 0; i < _tempBarks.Count; i++)
        {
            if (_tempBarks[i].name != null && _tempBarks[i].name.Contains(TrickManager.currentTrick.ToString()))
            {
                foundTrickBark = true;
                break;
            }
        }
        
        if (!foundTrickBark)
        {
            return;
        }

        // separate winning and losing barks
        foreach(Dialogue bark in _tempBarks)
        {
            if (bark.name == null) continue;
            
            if (bark.name.ToLower().Contains("win"))
            {
                _tempWinningBarks.Add(bark);
            }
            else
            {
                _tempLosingBarks.Add(bark);
            }
        }

        // play the right bark
        if (didPlayerWin)
        {
            if (_tempWinningBarks.Count == 1)
            {
                _dialogueManager.StartDialogue(_tempWinningBarks[0]);
            }
            else if (_tempWinningBarks.Count > 1)
            {
                // random bark of multiple
                _dialogueManager.StartDialogue(_tempWinningBarks[Random.Range(0, _tempWinningBarks.Count)]);
            }
        }
        else
        {
            if (_tempLosingBarks.Count == 1)
            {
                _dialogueManager.StartDialogue(_tempLosingBarks[0]);
            }
            else if (_tempLosingBarks.Count > 1)
            {
                // random bark of multiple
                _dialogueManager.StartDialogue(_tempLosingBarks[Random.Range(0, _tempLosingBarks.Count)]);
            }
        }

        currentBark++;
    }

    private IEnumerator SlowTrigger()
    {
        yield return new WaitForSeconds(.5f);
        TriggerDialogue();
    }
    
    // Helper methods to find specific dialogues by name or type
    public Dialogue FindDialogueByName(string dialogueName)
    {
        if (dialoguePhases == null) return null;
        
        foreach (Dialogue dialogue in dialoguePhases)
        {
            if (dialogue != null && dialogue.name == dialogueName)
            {
                return dialogue;
            }
        }
        
        Debug.LogWarning($"DialogueTrigger: Could not find dialogue with name '{dialogueName}'");
        return null;
    }
    
    public Dialogue GetWinDialogue()
    {
        if (dialoguePhases == null || dialoguePhases.Length == 0) return null;
        
        // Look for a dialogue specifically marked as win dialogue
        foreach (Dialogue dialogue in dialoguePhases)
        {
            if (dialogue != null && dialogue.name != null && 
                dialogue.name.ToLower().Contains("win") && !dialogue.isBark)
            {
                return dialogue;
            }
        }
        
        // Fallback: use second to last dialogue (original logic)
        if (dialoguePhases.Length >= 2)
        {
            return dialoguePhases[dialoguePhases.Length - 2];
        }
        
        return null;
    }
    
    public Dialogue GetLoseDialogue()
    {
        if (dialoguePhases == null || dialoguePhases.Length == 0) return null;
        
        // Look for a dialogue specifically marked as lose dialogue
        foreach (Dialogue dialogue in dialoguePhases)
        {
            if (dialogue != null && dialogue.name != null && 
                dialogue.name.ToLower().Contains("lose") && !dialogue.isBark)
            {
                return dialogue;
            }
        }
        
        // Fallback: use last dialogue (original logic)
        return dialoguePhases[dialoguePhases.Length - 1];
    }

    /// <summary>
    /// event Subscriptions
    /// </summary>
    private void OnEnable()
    {
        DialogueEvents.OnTriggeredDialogue += TriggerDialogue;
        GameEvents.OnTrickEnd += TriggerBark;
    }

    private void OnDisable()
    {
        DialogueEvents.OnTriggeredDialogue -= TriggerDialogue;
        GameEvents.OnTrickEnd -= TriggerBark;
    }
}
