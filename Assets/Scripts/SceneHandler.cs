using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneHandler : MonoBehaviour
{
    public static string FindScene(Button button)
    {
        return button.name;
    }

    public static void LoadScene(string scene)
    {
        if(SceneManager.GetSceneByName(scene) != null)
        {
            SceneManager.LoadScene(scene);
        }
        else
        {
            // google form doc
            Application.OpenURL("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
        }

    }
}
