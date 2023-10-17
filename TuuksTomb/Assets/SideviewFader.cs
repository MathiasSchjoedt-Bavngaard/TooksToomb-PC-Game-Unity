using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SideviewFader : MonoBehaviour
{
    public float xScale = 0.05f;
    public float yScale = 0.05f;
    private float colorScale = 0.1f;
    public float ownSideviewNumber;
    public float sceneSideviewNumber;

    public bool seetrhough = false;
    public GameObject[] ObjectsToTransform;

    // Start is called before the first frame update
    void Start()
    {
        //set color to 10% opacity and 10% darker for each Difference in sideviewNumber
        var sideviewDif = sceneSideviewNumber - ownSideviewNumber;
        if (sideviewDif != 0)
        {
            foreach (var gameObject in ObjectsToTransform)
            {
                var color = gameObject.GetComponent<Tilemap>().color;
                if (seetrhough)
                {
                    color.a = color.a - (colorScale * sideviewDif);
                }
                
                color.r = color.r - (colorScale * sideviewDif);
                color.g = color.g - (colorScale * sideviewDif);
                color.b = color.b - (colorScale * sideviewDif);
                gameObject.GetComponent<Tilemap>().color = color;

                //set order in layer to 1 less pr difference in sideviewNumber
                var order = gameObject.GetComponent<TilemapRenderer>().sortingOrder;
                order = (int)(order - sideviewDif);
                gameObject.GetComponent<TilemapRenderer>().sortingOrder = order;
            }


            //set the scale of this object to 5% less pr difference in sideviewNumber
            var scale = transform.localScale;
            scale.x = scale.x - (xScale * sideviewDif);
            scale.y = scale.y - (yScale * sideviewDif);
            transform.localScale = scale;


            //remove colliders for objects in other sideviews
            foreach (var gameObject in ObjectsToTransform)
            {
                if (gameObject.GetComponent<BoxCollider2D>() != null)
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }

                if (gameObject.GetComponent<CircleCollider2D>() != null)
                {
                    gameObject.GetComponent<CircleCollider2D>().enabled = false;
                }

                if (gameObject.GetComponent<PolygonCollider2D>() != null)
                {
                    gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                }

                if (gameObject.GetComponent<TilemapCollider2D>() != null)
                {
                    gameObject.GetComponent<TilemapCollider2D>().enabled = false;
                }
            }
        }
    }
}