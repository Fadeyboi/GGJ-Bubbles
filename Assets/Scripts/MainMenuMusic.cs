using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    public AudioSource src;
    public AudioClip clip;

    void Start()
    {
        // Ensure the AudioSource has the clip and starts playing
        if (src.clip != clip)
        {
            src.clip = clip;
        }

        if (!src.isPlaying)
        {
            src.Play();
        }
    }
}
