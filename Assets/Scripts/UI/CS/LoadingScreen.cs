using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadingScreen : UIRoot
{
    [SerializeField] private VisualElement _throbber;

    new void Start()
    {
        _throbber = _uiDoc.rootVisualElement.Q("Throber");
    }

    public void LoadNextScene(int delay, string sceneName)
    {
        // fade to black
        ToggleActivate(true);

        // start the load
        float time = Time.time;
        float endTime = Time.time + delay;

        while (time < endTime)
        {
            time += Time.deltaTime;
        }
        SceneHandler.LoadScene(sceneName);
    }
}
