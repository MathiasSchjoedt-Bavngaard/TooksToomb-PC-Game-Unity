using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var doorOpen = PlayerPrefs.GetInt("doorOpen");
        if (doorOpen == 1)
        {
            //open door
            gameObject.SetActive(false);
        }else
        {
            gameObject.SetActive(true);
        }
    }
    
}
