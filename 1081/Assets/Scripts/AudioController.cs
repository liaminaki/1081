using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour
{
    public float fadeInDuration;
    public float fadeOutDuration;

    public AudioSource audioSource;
    public AudioClip audioClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found.");
            return;
        }

        // Set the audio clip if it's not already set
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
        }

        PlayClip(audioClip);
        StartFadeIn();
    }

    public IEnumerator FadeIn()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found.");
            yield break;
        }

        float startVolume = audioSource.volume;
        audioSource.volume = 0f;

        while (audioSource.volume < startVolume)
        {
            audioSource.volume += Time.deltaTime / fadeInDuration;
            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found.");
            yield break;
        }

        float startVolume = audioSource.volume;

        while (audioSource.volume > 0f)
        {
            audioSource.volume -= Time.deltaTime / fadeOutDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    // Set the audio clip and play the sound
    public void PlayClip(AudioClip clip)
    {
        audioClip = clip;
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    // Call to start fade-in effect
    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    // Call to start fade-out effect
    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }
}