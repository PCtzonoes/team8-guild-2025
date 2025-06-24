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

    private void Start()
    {
        // set default delays
        for (int i = 0; i < dialoguePhases.Length; i++)
        {
            Dialogue dialogue = dialoguePhases[i];
            for (int j = 0; j < dialogue.dialogueLines.Length; j++)
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
        if (_startOnLoad == true)
        {
            StartCoroutine(SlowTrigger());
        }
    }

    // load the first dialogue collection
    public void TriggerDialogue()
    {
        while (dialoguePhases[_currentPhase - 1].isBark == true)
        {
            _currentPhase++;
        }
        FindObjectOfType<DialogueManager>().StartDialogue(dialoguePhases[_currentPhase-1]);
        //Debug.Log(_currentPhase);
        _currentPhase++;
        if(_currentPhase > dialoguePhases.Length)
        {
            _currentPhase--;
        }
    }

    // load the first dialogue collection
    public void TriggerDialogue(int phase)
    {
        while (dialoguePhases[phase - 1].isBark)
        {
            _currentPhase++;
        }
        FindObjectOfType<DialogueManager>().StartDialogue(dialoguePhases[phase - 1]);
        _currentPhase++;
        if (_currentPhase > dialoguePhases.Length)
        {
            _currentPhase--;
        }
    }

    public void TriggerBark(bool didPlayerWin)
    {
        List<Dialogue> barks = new List<Dialogue>();

        // find all dialogues that are the current bark
        foreach (Dialogue dialogue in dialoguePhases)
        {
            if (dialogue.isBark == true)
            {
                if (dialogue.name.Contains(currentBark.ToString()))
                {
                    barks.Add(dialogue);
                }
            }
        }

        // if there isn't a bark for this trick, don't call it
        for(int i = 0; i < barks.Count; i++)
        {

            if (barks[i].name.Contains(TrickManager.currentTrick.ToString()))
            {
                break;
            }
        }

        // separate winning and losing barks
        List<Dialogue> winningBarks = new List<Dialogue>();
        List<Dialogue> losingBarks = new List<Dialogue>();
        foreach(Dialogue bark in barks)
        {
            if (bark.name.ToLower().Contains("win"))
            {
                winningBarks.Add(bark);
            }
            else
            {
                losingBarks.Add(bark);
            }
        }

        // play the right bark
        if (didPlayerWin)
        {
            //Debug.Log(didPlayerWin);
            if (winningBarks.Count == 1)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(winningBarks[0]);
            }
            else
            {
                // random bark of multiple
                FindObjectOfType<DialogueManager>().StartDialogue(winningBarks[Random.Range(0, winningBarks.Count)]);
            }
        }
        else
        {
            if (losingBarks.Count == 1)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(losingBarks[0]);
            }
            else
            {
                // random bark of multiple
                FindObjectOfType<DialogueManager>().StartDialogue(losingBarks[Random.Range(0, losingBarks.Count)]);
            }
        }

        currentBark++;
    }

    private IEnumerator SlowTrigger()
    {
        yield return new WaitForSeconds(.5f);
        TriggerDialogue();
    }

    /// <summary>
    /// event Subscriptions
    /// </summary>
    private void OnEnable()
    {
        DialogueEvents.OnTriggeredDialogue += TriggerDialogue;
        GameEvents.OnTrickEnd += TriggerBark;
        DialogueEvents.OnWinDialogue += () => TriggerDialogue(dialoguePhases.Length - 1);
        DialogueEvents.OnLoseDialogue += () => TriggerDialogue(dialoguePhases.Length);
        //GameEvents.OnGameWon += TriggerDialogue;
    }

    private void OnDisable()
    {
        DialogueEvents.OnTriggeredDialogue -= TriggerDialogue;
        GameEvents.OnTrickEnd -= TriggerBark;
        DialogueEvents.OnWinDialogue -= () => TriggerDialogue(dialoguePhases.Length - 1);
        DialogueEvents.OnLoseDialogue -= () => TriggerDialogue(dialoguePhases.Length);
        //GameEvents.OnGameLost -= TriggerDialogue;
        //GameEvents.OnGameWon -= TriggerDialogue;
    }
}
