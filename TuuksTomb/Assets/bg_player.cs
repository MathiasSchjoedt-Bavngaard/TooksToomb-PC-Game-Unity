using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgMusic : MonoBehaviour
{
    public AudioClip bg_clip;
    private AudioSource bg_music;
    private string currentSceneName;

    // Define a dictionary to map scene names to music tracks
    public AudioClip[] sceneMusicTracks;

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


        // Initialize the current scene name
        currentSceneName = SceneManager.GetActiveScene().name;

        // Play the corresponding music track for the current scene
        SelectSceneMusic(currentSceneName);

        // Subscribe to the scene change event
        SceneManager.sceneLoaded += OnSceneLoaded;

        Debug.Log("print is working");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Update the current scene name
        currentSceneName = scene.name;
        Debug.Log(currentSceneName);
        // Play the corresponding music track for the new scene
        SelectSceneMusic(currentSceneName);
    }

    void SelectSceneMusic(string sceneName)
    {
        switch (sceneName)
        {
            case "TitleScreen":
                bg_clip = sceneMusicTracks[0];
                break;
            case "TopDown1":
                bg_clip = sceneMusicTracks[0];
                break;
            case "Sideview1.1":
                bg_clip = sceneMusicTracks[0];
                break;
            case "Sideview2.1":
                bg_clip = sceneMusicTracks[1];
                break;
            case "TopDown2":
                bg_clip = sceneMusicTracks[1];
                break;
            case "Sideview3.1":
                bg_clip = sceneMusicTracks[2];
                break;
            case "TopDown3":
                bg_clip = sceneMusicTracks[2];
                break;

            default:
                Debug.LogWarning("Invalid input: " + sceneName);
                break;
        }

        PlayMusicAndCheck(bg_clip);


    }

    private void PlayMusicAndCheck(AudioClip clip)
    {
        // Check if the current track is the same as the target track
        if (bg_music.clip != clip || !bg_music.isPlaying)
        {
            bg_music.clip = clip;
            bg_music.Play();
        }

    }


}
