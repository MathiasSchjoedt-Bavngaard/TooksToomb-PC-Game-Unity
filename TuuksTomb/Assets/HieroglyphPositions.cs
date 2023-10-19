using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HieroglyphPositions : MonoBehaviour
{
    public GameObject[] hieroglyphs;
    public bool sideView = true;
    public float zPosition = 0;
    
    // Start is called before the first frame update
    private void SaveHieroglyphPositions()
    {
        
        foreach (var hieroglyph in hieroglyphs)
        {
            var hieroglyphPosition = hieroglyph.transform.position;
            var hieroglyphX = hieroglyphPosition.x;
            var hieroglyphY = hieroglyphPosition.y;
            
            PlayerPrefs.SetFloat(hieroglyph.name + "x", hieroglyphX);
            if(sideView)
            {
                PlayerPrefs.SetFloat(hieroglyph.name + "y", hieroglyphY);
            }
            else
            {
                PlayerPrefs.SetFloat(hieroglyph.name + "z", hieroglyphY);
            }
           
        }
    }
    
    // Making hieroglyphs visible and setting their positions
    private void SetHeirpglyphPositions()
    {
        if (sideView)
        {
            foreach (var hieroglyph in hieroglyphs)
            {
                var heirglyphZ = PlayerPrefs.GetFloat(hieroglyph.name + "z");
                
                //if heirglyphZ is in between 1 of zPosition set hieroglyph position to x and y else disable hieroglyph
                if (zPosition  >= heirglyphZ -0.5 && zPosition < heirglyphZ + 0.5)
                {
                    PlayerPrefs.SetFloat(hieroglyph.name + "z", zPosition);
                    
                    //if hieroglyph dont have positions set them
                    if (!PlayerPrefs.HasKey(hieroglyph.name + "y"))
                    {
                        var hieroglyphPosition = hieroglyph.transform.position;
                        var hieroglyphYfirst = hieroglyphPosition.y;
                        PlayerPrefs.SetFloat(hieroglyph.name + "y", hieroglyphYfirst);
                    }
                    
                    
                    
                    hieroglyph.SetActive(true);
                    var hieroglyphX = PlayerPrefs.GetFloat(hieroglyph.name + "x");
                    var hieroglyphY = PlayerPrefs.GetFloat(hieroglyph.name + "y");
                    hieroglyph.transform.position = new Vector2(hieroglyphX, hieroglyphY);
                }
                else
                {
                    hieroglyph.SetActive(false);
                }
            }
        }
        //else set hieroglyph to topdown. X=x and z=y
        else
        {
            foreach (var hieroglyph in hieroglyphs)
            {
                
                //if not player prefs set them
                if (!PlayerPrefs.HasKey(hieroglyph.name + "x"))
                    PlayerPrefs.SetFloat(hieroglyph.name + "x", hieroglyph.transform.position.x);
                if (!PlayerPrefs.HasKey(hieroglyph.name + "z"))
                    PlayerPrefs.SetFloat(hieroglyph.name + "z", hieroglyph.transform.position.y);
                
                     
                      
                hieroglyph.SetActive(true);
                var hieroglyphX = PlayerPrefs.GetFloat(hieroglyph.name + "x");
                var hieroglyphZ = PlayerPrefs.GetFloat(hieroglyph.name + "z");
                hieroglyph.transform.position = new Vector2(hieroglyphX, hieroglyphZ);
            }
        }
    }

    private void Start()
    {
        SetHeirpglyphPositions();
    }

    private void Update()
    {
        //if z key pressed Save hieroglyph positions
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SaveHieroglyphPositions();
        }
        
        
        //if x key pressed Set hieroglyph positions
        if(Input.GetKeyDown(KeyCode.B))
           SetHeirpglyphPositions();
        
        if(Input.GetKeyDown(KeyCode.R))
            PlayerPrefs.DeleteAll();
    }
}
