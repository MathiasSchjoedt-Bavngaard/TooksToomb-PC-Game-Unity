using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//timer counts up from 0 at the start of game
public class GameTimer : MonoBehaviour
{
    // initialize timerText with Hurry Up! text
    public UnityEngine.UI.Text timerText;
    
    
    private static float startTime = 0;
    private static bool running = false;
    private static bool finished = false;
    
    // Start is called before the first frame update
    void Start()
    {
        TimeSpan time = TimeSpan.FromSeconds(startTime);
        timerText.text = time.ToString(@"mm\:ss\:ff");
    }
    
    // Update is called once per frame counting text up from 0
    void Update()
    {
        if (!finished )
        {
            startTime += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(startTime);
            timerText.text = time.ToString(@"mm\:ss\:ff");
        }
    }
    
    //on trigger stop timer and go to end Scene 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        StopTimer();
        PlayerPrefs.DeleteAll();
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
    }
    
    //stop timer
    public void StopTimer()
    {
        finished = true;
        running = false;
    }
    
    public void RestartGame()
    {
        PlayerPrefs.DeleteAll();
        UnityEngine.SceneManagement.SceneManager.LoadScene("TopDown1"); //this should be more dynamic to "start" scene
        startTime = 0;
        finished = false;
        running = true;
    }

    public static void ResetTimer()
    {
        startTime = 0;
        finished = false;
        running = true;
    }
}
