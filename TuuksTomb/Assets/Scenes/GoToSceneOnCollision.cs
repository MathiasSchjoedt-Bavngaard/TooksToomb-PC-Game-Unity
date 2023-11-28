using System.Collections;
using System.Collections.Generic;
using Eflatun.SceneReference;
using UnityEngine;

public class GoToSceneOnCollision : MonoBehaviour
{
    public SceneReference goToScene;
    public Vector2 initialPosition;
    public bool sideView = false;

    //on trigger stop timer and go to end Scene 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        PlayerPrefs.SetString("continueLevel", goToScene.Name);
        
        PlayerPrefs.SetFloat("x", initialPosition.x);
        if (sideView)
            PlayerPrefs.SetFloat("y", initialPosition.y);
        else
        PlayerPrefs.SetFloat("z", initialPosition.y);
        UnityEngine.SceneManagement.SceneManager.LoadScene(goToScene.Name);
    }
}