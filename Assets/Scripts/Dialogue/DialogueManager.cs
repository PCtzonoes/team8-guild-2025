using DefaultNamespace.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    private Dialogue _dialogue;

    private GameManager _gameManager;

    private DialogueMenu _dialogueMenu;

    private int _currentLine = -1;
   
    //private bool _dialogueActive = false;

    private void Start()
    {
        // declare the dialogue UI
        _dialogueMenu = FindObjectOfType<DialogueMenu>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log(_dialogue);
            //Debug.Log(_dialogueMenu.scrolling);
            //Debug.Log(_dialogueMenu.active);
            //if (_dialogueActive == false) { Debug.LogWarning(_dialogueActive); return; }
            if (_dialogueMenu.active == false) return;
            DisplayCurrentLine(_dialogueMenu.scrolling); 
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        // load all data from dialogue
        _dialogue = dialogue;
        //_dialogueActive = true;

        // display the first line with the data loaded
        DisplayCurrentLine(false);
    }

    public void DisplayCurrentLine(bool scrolling)
    {
        // if we're at the end of the dialogue sequence, end the dialogue
        if (scrolling == true)
        {
            StopAllCoroutines();
            _dialogueMenu.scrolling = false;
            _dialogueMenu.UpdateLine(_dialogue.dialogueLines[_currentLine].line);
        }
        else
        {
            _currentLine++;
            if (_currentLine >= _dialogue.dialogueLines.Length)
            {
                EndDialogue();
                //Debug.Log(_currentLine + "is current");
                return;
            }
            string line = _dialogue.dialogueLines[_currentLine].line;
            _dialogueMenu.ToggleActivate(true);
            StartCoroutine(ScrollText(line));
        }
    }

    private IEnumerator ScrollText(string line)
    {
        _dialogueMenu.scrolling = true;
        _dialogue.dialogueLines[_currentLine].OnLineStart.Invoke();

        try
        {
            // make the UI blank
            string partialLine = "";
            _dialogueMenu.UpdateLine(partialLine);

            // apply the letter delay for this line
            float letterDelay = _dialogue.dialogueLines[_currentLine].letterDelay;
            
            // wait to load this line by this line's start delay
            yield return new WaitForSeconds(_dialogue.dialogueLines[_currentLine].startDelay);

            for (int i = 0; i < line.Length; i++)
            {
                char newChar = line[i];
                partialLine += newChar;
                _dialogueMenu.UpdateLine(partialLine);

                if (newChar.ToString() == ",")
                {
                    yield return new WaitForSeconds(letterDelay * 2);
                }
                else if (newChar.ToString() == "." || newChar.ToString() == "?" || newChar.ToString() == ".")
                {
                    yield return new WaitForSeconds(letterDelay * 3);
                }
                else
                {
                    yield return new WaitForSeconds(letterDelay);
                }
            }
        }
        finally
        {
            _dialogueMenu.scrolling = false;
        }
    }

    // turn off the UI
    private void EndDialogue()
    {
        // reset all values
        _currentLine = -1;
        _dialogueMenu.ToggleActivate(false);
        _dialogue.OnDialogueEnd.Invoke();
        if (_dialogue.isFinal == false)
        {
            _gameManager.CheckRoundOverState();
        }
        //_gameManager.CheckRoundOverState();
        //_dialogue = null;
    }
}
