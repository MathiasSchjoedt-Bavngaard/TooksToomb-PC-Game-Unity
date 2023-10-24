using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TjeckDoorOpen : MonoBehaviour
{
    public GameObject closedDoor;
    public GameObject openDoor;
    public GameObject openDoorFloor;
    public GameObject openDoorTriggerBox;
    // Start is called before the first frame update
    void Start()
    {
        
    var doorOpen = PlayerPrefs.GetInt("doorOpen");
    if (doorOpen == 1)
    {
        //open door
        closedDoor.SetActive(false);
        openDoor.SetActive(true);
        openDoorFloor.SetActive(true);
        openDoorTriggerBox.SetActive(true);
        
    }
    else
    {
        closedDoor.SetActive(true);
        openDoor.SetActive(false);
        openDoorFloor.SetActive(false);
        openDoorTriggerBox.SetActive(false);
    }
        

    }
}
