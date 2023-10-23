using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HieroglyphInPlace : MonoBehaviour
{
   
    public float[] xPositions;
    public float[] yPositions;
    public GameObject[] hieroglyphs;
    public GameObject[] objectsToMakeGlow;
    
    
    private void setDoorOpenIfHeiroglyhsIsCorrect()
    {
        var correct = true;
        
        // if hieroglyphs are in correct inside 0.5 of a unit set doorOpen to 1
        for (int i = 0; i < hieroglyphs.Length; i++)
        {
            var hieroglyphPosition = hieroglyphs[i].transform.position;
            var xPosition = xPositions[i];
            var yPosition = yPositions[i];
            if (hieroglyphPosition.x > xPosition + 0.5 || hieroglyphPosition.x < xPosition - 0.5)
            {
                correct = false;
                break;
            }
            if (hieroglyphPosition.y > yPosition + 0.5 || hieroglyphPosition.y < yPosition - 0.5)
            {
                correct = false;
                break;
            }
        } 

        if (correct)
        {
            foreach (var obj in objectsToMakeGlow)
            {
                obj.SetActive(true);
            }
            PlayerPrefs.SetInt("doorOpen", 1);
          
        }
        else
        {
            foreach (var obj in objectsToMakeGlow)
            {
                obj.SetActive(false);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        setDoorOpenIfHeiroglyhsIsCorrect();
    }
    
    void Start()
    {   
        //everySecond check if hieroglyphs are in correct position
        InvokeRepeating(nameof(setDoorOpenIfHeiroglyhsIsCorrect), 0.0f, 1.0f);
        
    }
}
