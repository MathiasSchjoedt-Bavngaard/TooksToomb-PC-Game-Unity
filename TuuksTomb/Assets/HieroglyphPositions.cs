using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HieroglyphPositions : MonoBehaviour
{
    public GameObject[] hieroglyphs;
    public bool sideView = true;
    public float zPosition = -1000;
    private const float ColorScale = 0.1f;
    public float xScale = 0.05f;
    public float yScale = 0.05f;

    // Start is called before the first frame update
    private void SaveHieroglyphPositions()
    {
        foreach (var hieroglyph in hieroglyphs)
        {
            if (PlayerPrefs.HasKey(hieroglyph.name + "z") == false)
                PlayerPrefs.SetFloat(hieroglyph.name + "z", hieroglyph.transform.position.y);

            if (sideView)
            {
                var heiroglyphZ = PlayerPrefs.GetFloat(hieroglyph.name + "z");

                //if hieroglyph is not active or is further away than zPosition dont save it because it is not movable
                if (hieroglyph.activeSelf == false || (sideView && !HieroglyphMovable(heiroglyphZ))) continue;
            }

            var hieroglyphPosition = hieroglyph.transform.position;
            var hieroglyphX = hieroglyphPosition.x;
            var hieroglyphY = hieroglyphPosition.y;

            PlayerPrefs.SetFloat(hieroglyph.name + "x", hieroglyphX);
            if (sideView)
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
                var heiroglyphZ = PlayerPrefs.GetFloat(hieroglyph.name + "z");


                if (!HieroglyphVisible(heiroglyphZ))
                {
                    hieroglyph.SetActive(false);
                    continue;
                }

               

                //if hieroglyph dont have positions jet, set y position to current y position
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
                
                if (HieroglyphMovable(heiroglyphZ))
                { 
                    PlayerPrefs.SetFloat(hieroglyph.name + "z", zPosition);
                }
                else
                {
                    // make hieroglyph 5% smaller for each unit it is further away than zPosition
                    ScaleDownHieroglyph(hieroglyph, heiroglyphZ - zPosition);

                    //make hieroglyph 10% darker for each unit it is further away than zPosition
                    hieroglyph.GetComponent<SpriteRenderer>().color =
                        ScaledownColor(hieroglyph.GetComponent<SpriteRenderer>().color, heiroglyphZ - zPosition);

                    //make hieroglyph not falling
                    hieroglyph.GetComponent<Rigidbody2D>().gravityScale = 0;

                    //remove colliders
                    if (hieroglyph.GetComponent<BoxCollider2D>() != null)
                    {
                        hieroglyph.GetComponent<BoxCollider2D>().enabled = false;
                    }
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

        //TODO: remove input as only for testing
        //if x key pressed Set hieroglyph positions
        if (Input.GetKeyDown(KeyCode.B))
            SetHeirpglyphPositions();

        if (Input.GetKeyDown(KeyCode.R))
            PlayerPrefs.DeleteAll();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        SaveHieroglyphPositions();
    }

    private static Color ScaledownColor(Color color, float sideviewDif)
    {
        var procents = (ColorScale * sideviewDif);

        //set color to colorScale% darker for each Difference in sideviewNumber
        var redtoremove = color.r * procents;
        color.r = color.r - redtoremove;
        var greentoremove = color.g * procents;
        color.g = color.g - greentoremove;
        var bluetoremove = color.b * procents;
        color.b = color.b - bluetoremove;
        return color;
    }

    private void ScaleDownHieroglyph(GameObject hieroglyph, float sideviewDif)
    {
        //scale down hieroglyph for each unit it is further away than zPosition
        var scale = hieroglyph.transform.localScale;
        scale.x = scale.x - (xScale * sideviewDif);
        scale.y = scale.y - (yScale * sideviewDif);
        hieroglyph.transform.localScale = scale;
                
        //Move hieroglyph further away from camera
        var position = hieroglyph.transform.position;
        position.x = position.x * scale.x;
        position.y = position.y * scale.y;
        hieroglyph.transform.position = position;
        
        
    }

    //functions to tjeck hieroglyph active layer
    private bool HieroglyphVisible(float hieroglyphZ)
    {
        return zPosition - 0.5 <= hieroglyphZ;
    }

    private bool HieroglyphMovable(float hieroglyphZ)
    {
        return zPosition + 0.5 > hieroglyphZ;
    }
}