using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WorldMapMenu : UIRoot
{
    private List<Button> _levels;
    private List<Button> _gates;
    private List<VisualElement> _worlds;
    private VisualElement _map;

    [System.Obsolete]
    new void Start()
    {
        base.Start();
        // delcare custom elements
        _levels = new List<Button>();
        _gates = new List<Button>();
        _worlds = new List<VisualElement>();
        _map = _uiDoc.rootVisualElement.Q("map");

        // get all the worlds
        List<VisualElement> allVE = _map.Query().ToList();
        foreach (VisualElement ve in allVE)
        {
            if (ve.ClassListContains("world"))
            {
                _worlds.Add(ve);
            }
        }

        // get all the levels and gates
        List<Button> allButtons = _map.Query<Button>().ToList();
        foreach (Button button in allButtons)
        {
            if (button.ClassListContains("level"))
            {
                _levels.Add(button);
                button.clicked += () => OnLevelClick(button);
            }
            else if (button.ClassListContains("gate"))
            {
                _gates.Add(button);
                button.clicked += () => OnGateClick(button);
            }
        }
    }

    private void OnGateClick(Button button)
    {
        if (!button.focusable)
        {
            return;
        }

        VisualElement previousWorld = new VisualElement();
        VisualElement currentWorld = new VisualElement();
        VisualElement nextWorld = new VisualElement();

        for(int i = 0; i < _worlds.Count; i++)
        {
            if (_worlds[i].ClassListContains("world-inactive") == false)
            {
                if(i-1 >= 0)
                {
                    previousWorld = _worlds[i - 1];
                }

                currentWorld = _worlds[i];

                if (i + 1 < _worlds.Count)
                {
                    nextWorld = _worlds[i + 1];
                }

                break;
            }
        }

        // hide the current world
        currentWorld.AddToClassList("world-inactive");

        if (button.name.ToLower().Contains("entrance"))
        {
            previousWorld.RemoveFromClassList("world-inactive");
            // TODO change the map sprite 
        }
        else if (button.name.ToLower().Contains("exit"))
        {
            nextWorld.RemoveFromClassList("world-inactive");
            // TODO change the map sprite 
        }
    }

    [System.Obsolete]
    private void OnLevelClick(Button button)
    {
        if (!button.focusable)
        {
            return;
        }

        string levelName = SceneHandler.FindScene(button);
        if (levelName != null)
        {
            SceneHandler.LoadScene(levelName);
        }
    }

    public void ToggleButtonFocusable(string buttonName)
    {
        for(int i = 0;  i < _gates.Count; i++)
        {
            if (_gates[i].name == buttonName)
            {
                _gates[i].focusable = true;
                _gates[i].RemoveFromClassList("gate-locked");
                return;
            }
        }

        for (int i = 0; i < _levels.Count; i++)
        {
            if (_levels[i].name == buttonName)
            {
                _levels[i].focusable = true;
                _levels[i].RemoveFromClassList("level-locked");
                return;
            }
        }
    }
}
