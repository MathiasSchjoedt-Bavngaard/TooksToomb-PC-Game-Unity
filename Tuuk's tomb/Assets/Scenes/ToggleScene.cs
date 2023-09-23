using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ToggleScene : MonoBehaviour
{
    public VectorValue playerStorage;
    public GameObject player;

    private void Start()
    {
        playerStorage.initialValue = player.transform.position;
    }

    public void ToggleSceneFunction()
    {
        PlayerPrefs.SetFloat("x", player.transform.position.x);
        PlayerPrefs.SetFloat("y", player.transform.position.y);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name == "Sideview1.2" ? "TopDown1" : "Sideview1.2");
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
