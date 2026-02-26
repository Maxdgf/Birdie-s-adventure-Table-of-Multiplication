using UnityEngine;

public class OnClickButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip sound;
    [SerializeField] private AudioSource source;

    void Start()
    {
        source.clip = sound; // set sound effect to audio source
    }

    public void PlayOnClickSound()
    {
        source.Play(); // play sound effect
    }
}
