using System.Collections;
using UnityEngine;

public class CollisionSoundFade : MonoBehaviour
{
    public AudioClip collisionClip;
    public float fadeOutTime = 1f; // The duration of the fade-out in seconds

    private AudioSource audioSource;
    private Coroutine fadeOutCoroutine;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
        {
            Debug.LogError("No AudioSource component found on this GameObject!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (audioSource && collisionClip)
        {
            audioSource.PlayOneShot(collisionClip);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Start fading out the sound when the collision ends
        if (audioSource.isPlaying)
        {
            if (fadeOutCoroutine != null)
            {
                StopCoroutine(fadeOutCoroutine);
            }
            fadeOutCoroutine = StartCoroutine(FadeOut(audioSource, fadeOutTime));
        }
    }

    IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Reset volume to start value to not affect future playbacks
    }
}
