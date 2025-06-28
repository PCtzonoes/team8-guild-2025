using System.Collections;
using System.Text;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueEvents dialogueEvents;
    [SerializeField] private DialogueMenu _dialogueMenu;
    
    private Dialogue _dialogue;
    private int _currentLine = -1;
    
    private Coroutine _currentScrollCoroutine;
    private StringBuilder _stringBuilder = new StringBuilder();
    
    private void Start()
    {
        _dialogueMenu = FindObjectOfType<DialogueMenu>();
    }

    private void Update()
    {
        // Only process input if we have an active dialogue
        if (Input.GetMouseButtonDown(0) && _dialogue != null)
        {
            DisplayCurrentLine(_dialogueMenu.scrolling); 
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogue == null)
        {
            Debug.Log("[DialogueManager] Potato cannon");
            dialogueEvents.DialogueEnd();
            return;
        }
        
        // load all data from dialogue
        _dialogue = dialogue;
        
        // display the first line with the data loaded
        DisplayCurrentLine(false);
    }

    public void DisplayCurrentLine(bool scrolling)
    {
        if (_currentScrollCoroutine != null)
        {
            StopCoroutine(_currentScrollCoroutine);
        }
        
        // if we're at the end of the dialogue sequence, end the dialogue
        if (scrolling == true)
        {
            _dialogueMenu.scrolling = false;
            _dialogueMenu.UpdateLine(_dialogue.dialogueLines[_currentLine].line);
        }
        else
        {
            _currentLine++;
            if (_currentLine >= _dialogue.dialogueLines.Length)
            {
                EndDialogue();
                return;
            }
            
            string line = _dialogue.dialogueLines[_currentLine].line;
            _dialogueMenu.ToggleActivate(true);
            _currentScrollCoroutine = StartCoroutine(ScrollText(line));
        }
    }

    private IEnumerator ScrollText(string line)
    {
        _dialogueMenu.scrolling = true;
        
        try
        {
            _stringBuilder.Clear();
            _dialogueMenu.UpdateLine("");

            // apply the letter delay for this line
            float letterDelay = _dialogue.dialogueLines[_currentLine].letterDelay;
            
            // wait to load this line by this line's start delay
            yield return new WaitForSeconds(_dialogue.dialogueLines[_currentLine].startDelay);

            for (int i = 0; i < line.Length; i++)
            {
                char newChar = line[i];
                _stringBuilder.Append(newChar);
                _dialogueMenu.UpdateLine(_stringBuilder.ToString());

                float waitTime = letterDelay;
                if (newChar == ',')
                {
                    waitTime = letterDelay * 2;
                }
                else if (newChar == '.' || newChar == '?' || newChar == '!')
                {
                    waitTime = letterDelay * 3;
                }
                
                yield return new WaitForSeconds(waitTime);
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
        // Clean up coroutine reference
        if (_currentScrollCoroutine != null)
        {
            StopCoroutine(_currentScrollCoroutine);
        }
        
        // reset all values
        _currentLine = -1;
        _dialogueMenu?.ToggleActivate(false);
        _dialogue = null;
        dialogueEvents.DialogueEnd();
    }
    
    private void OnDestroy()
    {
        if (_currentScrollCoroutine != null)
        {
            StopCoroutine(_currentScrollCoroutine);
        }
    }
    
    private void OnDisable()
    {
        if (_currentScrollCoroutine != null)
        {
            StopCoroutine(_currentScrollCoroutine);
        }
    }
}
