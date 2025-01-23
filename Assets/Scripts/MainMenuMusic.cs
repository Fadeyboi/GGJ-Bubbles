using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MainMenuMusic : MonoBehaviour
{
    public AudioSource src;
    public AudioClip clip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        src.clip = clip;
        src.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
