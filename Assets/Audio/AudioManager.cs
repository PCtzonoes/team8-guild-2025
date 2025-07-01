using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    private static EventReference _testSFX = RuntimeManager.PathToEventReference("event:/TestSFX");

    public static void PlayTestSFX()
    {
        RuntimeManager.PlayOneShot(_testSFX);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayTestSFX();
        }
    }
}