using UnityEngine;

public class AudioManagerLazy : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
            _audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _clips.Length > 0)
        {
            var clip = _clips[Random.Range(0, _clips.Length)];
            _audioSource.PlayOneShot(clip);
        }
    }
}