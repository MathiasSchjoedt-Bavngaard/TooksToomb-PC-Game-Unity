using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Player : MonoBehaviour
{
    [SerializeField] private AudioSource footStepSound;
    public List<AudioClip> footSteps;
    // Update is called once per frame

    float timer = 0f;
    int oldClipNumber = -1;

    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            timer += Time.deltaTime;
            float deltaTime = Random.Range(0.3f, 0.6f);
            if (timer > deltaTime)
            {
                footStepSound.enabled = true;

                int clipNumber = Random.Range(0, footSteps.Count);
                if (clipNumber == oldClipNumber)
                {
                    clipNumber++;
                    if (clipNumber > footSteps.Count)
                    {
                        clipNumber = 0;
                    }
                }
                AudioClip footClip = footSteps[clipNumber];
                footStepSound.clip = footClip;
                oldClipNumber = clipNumber;
                footStepSound.volume = Random.Range(0.3f, 0.5f);
                footStepSound.Play();

                timer = 0f;
            }
            
            
        }
        
        
    }
}
