using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Eflatun.SceneReference;
using UnityEngine.SceneManagement;
public class ToggleScene : MonoBehaviour
{
    public SceneReference Topdown;
    public List<SceneReference> Sideviews;
    public float maxY;
    public float minY;
    
    public GameObject player;
    private void ToggleSceneFunction()
    {
        var playerPosition = player.transform.position;
        PlayerPrefs.SetFloat("x", playerPosition.x);
        var currentScene = SceneManager.GetActiveScene();

        var sceneTriggers = new float[Sideviews.Count];
        var deltaY = maxY - minY;
        
        for (int i = 0; i < Sideviews.Count; i++)
        {
            sceneTriggers[i] = maxY - ((deltaY / Sideviews.Count) * (i + 1));
        }
        
        if( currentScene.name == Topdown.Name)
        {
            PlayerPrefs.SetFloat("z", playerPosition.y);
            
            for (int i = 0; i < Sideviews.Count; i++)
            {
                if (playerPosition.y > sceneTriggers[i])
                {
                    SceneManager.LoadScene(Sideviews[i].Name);
                    break;
                }
            }
        }
        
        else
        {
            PlayerPrefs.SetFloat("y", player.transform.position.y);
            SceneManager.LoadScene(Topdown.Name);
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
