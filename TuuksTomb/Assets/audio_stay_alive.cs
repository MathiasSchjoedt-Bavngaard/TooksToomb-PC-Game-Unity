using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_stay_alive : MonoBehaviour
{
    private void Awake()
    {
        // Make this GameObject persist between scenes
        DontDestroyOnLoad(gameObject);
    }
}
