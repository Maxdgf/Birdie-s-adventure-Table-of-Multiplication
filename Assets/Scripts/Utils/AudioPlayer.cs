/*
 * Description
 * -------------------------------------------------------------------
 * This script simplifies setup and management of scene audio sources.
 */

using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource; // current audio source

    /// <summary>
    /// Plays specific audio clip.
    /// </summary>
    /// <param name="audioClip">Audio clip.</param>
    public void PlayAudio(AudioClip audioClip)
    {
        if (!IsCurrentAudioClipAlreadySetted(audioClip)) audioSource.clip = audioClip;
        audioSource.Play();
    }

    /// <summary>
    /// Plays specific audio clip after delay.
    /// </summary>
    /// <param name="audioClip">Audio clip.</param>
    /// <param name="delay">Play delay.</param>
    public IEnumerator PlayAudioAfterDelay(AudioClip audioClip, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!IsCurrentAudioClipAlreadySetted(audioClip)) audioSource.clip = audioClip; // set audio clip
        audioSource.Play();
    }

    /// <summary>
    /// Sets audio source.
    /// </summary>
    /// <param name="source">Audio source.</param>
    public void SetAudioSource(AudioSource source)
    {
        audioSource = source; // setup audio source
    }

    /// <summary>
    /// Cheks is audio clip already setted to audio source.
    /// </summary>
    /// <param name="selectedClip">Selected audio clip.</param>
    /// <returns>Boolean state.</returns>
    private bool IsCurrentAudioClipAlreadySetted(AudioClip selectedClip)
    {
        if (audioSource.clip == selectedClip) return true;
        else return false;
    }
}
