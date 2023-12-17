using System.Collections;
using UnityEngine;

public class CollisionSoundFade : MonoBehaviour
{
    public AudioClip collisionClip;
    public float fadeOutTime = 1f; // The duration of the fade-out in seconds

    public AudioSource audioSource;
    private Coroutine _fadeOutCoroutine;
    private float _startVolume;

    void Start()
    {
        _startVolume = audioSource.volume;
        if (audioSource) return;
        Debug.LogError("No AudioSource component found on this GameObject!");
    }

    void OnCollisionEnter2D(Collision2D collidingObject)
    {
        if (!collidingObject.gameObject.CompareTag("Player")) return;
        if (!audioSource || !collisionClip) return;
        if (_fadeOutCoroutine != null)
        {
            StopCoroutine(_fadeOutCoroutine);
        }

        audioSource.volume = _startVolume;

        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(collisionClip);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Start fading out the sound when the collision ends
        if (!audioSource.isPlaying) return;
        if (_fadeOutCoroutine != null)
        {
            StopCoroutine(_fadeOutCoroutine);
        }

        _fadeOutCoroutine = StartCoroutine(FadeOut(audioSource, fadeOutTime));
    }

    private IEnumerator FadeOut(AudioSource methodAudioSource, float fadeTime)
    {
        while (methodAudioSource.volume > 0)
        {
            methodAudioSource.volume -= _startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        methodAudioSource.Stop();
        methodAudioSource.volume = _startVolume; // Reset volume to start value to not affect future playbacks
    }
}