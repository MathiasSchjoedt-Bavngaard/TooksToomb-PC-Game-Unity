using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMusic : MonoBehaviour
{
    public AudioClip bg_clip;
    private AudioSource bg_music;

    private void Awake()
    {
        // Make this GameObject persist between scenes
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        bg_music = GetComponent<AudioSource>();

        // Set the audio clip to play
        bg_music.clip = bg_clip;
        bg_music.Play();
    }

    private void Update()
    {

    }
}