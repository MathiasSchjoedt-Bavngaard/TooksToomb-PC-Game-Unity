using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnKeyPress : MonoBehaviour
{
    public AudioClip soundToPlay;
    public AudioSource audioSource;

    private void Awake()
    {
        // Make this GameObject persist between scenes
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        // Get the AudioSource component attached to the same GameObject
       

        // Set the audio clip to play
        audioSource.clip = soundToPlay;
    }

    private void Update()
    {
        // Check if the Space key is pressed
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // Play the sound
            audioSource.Play();
        }
    }
}