using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ToggleScene : MonoBehaviour
{
    public GameObject player;
    private void ToggleSceneFunction()
    {
        var playerPosition = player.transform.position;
        PlayerPrefs.SetFloat("x", playerPosition.x);
        
        if(SceneManager.GetActiveScene().name == "TopDown1")
        {
            PlayerPrefs.SetFloat("z", playerPosition.y);
            
            switch (playerPosition.y)
            {
                //in case player y is inbeween 10 and 0 go to scene 1.1 
                case < 10 and > 0:
                    SceneManager.LoadScene("Sideview1.1");
                    break;
                
                //in case player y is inbeween 0 and -10 go to scene 1.2
                case > -10 and < 0:
                    SceneManager.LoadScene("Sideview1.2");
                    break;
            }
        }
        else
        {
            PlayerPrefs.SetFloat("y", player.transform.position.y);
            SceneManager.LoadScene("TopDown1");
        }
    }
    

    public void Update()
    { 
        //if z key pressed toggle view 
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ToggleSceneFunction();
        }
    }
}
