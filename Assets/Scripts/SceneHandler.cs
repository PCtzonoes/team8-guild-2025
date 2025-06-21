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
        Scene next = SceneManager.GetSceneByName(scene);
        if (next.IsValid())
        {
            SceneManager.LoadScene(scene);
        }
        else
        {
            // google form doc
            Application.OpenURL("https://forms.gle/gxkvKNxVSeUw5Emp6");
        }

    }
}
