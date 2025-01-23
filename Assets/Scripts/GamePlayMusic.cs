using UnityEngine;

public class GamePlayMusic : MonoBehaviour
{
    // 1) Expose the AudioClip in the Inspector
    public AudioClip musicClip;

    // 2) We'll store our AudioSource in a private variable
    private AudioSource _audioSource;

    void Awake()
    {
        // 3) Attempt to get an existing AudioSource on this GameObject
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            // If there's no AudioSource, add one at runtime
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 4) Assign the public clip to the AudioSource
        _audioSource.clip = musicClip;
        _audioSource.loop = true;         // Loop the music
        _audioSource.playOnAwake = false; // We’ll control when it plays

        // 5) Start playing the clip
        if (_audioSource.clip != null)
        {
            _audioSource.Play();
        }
    }
}
