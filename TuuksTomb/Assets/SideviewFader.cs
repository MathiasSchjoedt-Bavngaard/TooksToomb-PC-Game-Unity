using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class SideviewFader : MonoBehaviour
{
    public float xScale = 0.05f;
    public float yScale = 0.05f;
    private const float ColorScale = 0.1f;
    public float ownSideviewNumber;
    public float sceneSideviewNumber;

    public bool seetrhough = false;
    public GameObject[] objectsToTransform;

    // Start is called before the first frame update
    void Start()
    {
        //set color to 10% opacity and 10% darker for each Difference in sideviewNumber
        var sideviewDif = sceneSideviewNumber - ownSideviewNumber;
        if (sideviewDif != 0)
        {
            foreach (var objectToTransform in objectsToTransform)
            {
                var color = objectToTransform.GetComponent<Tilemap>().color;
                if (seetrhough)
                {
                    color.a = color.a - (ColorScale * sideviewDif);
                }
                objectToTransform.GetComponent<Tilemap>().color = ScaledownColor(color, sideviewDif);

                //set order in layer to 1 less pr difference in sideviewNumber
                var order = objectToTransform.GetComponent<TilemapRenderer>().sortingOrder;
                order = (int)(order - sideviewDif);
                objectToTransform.GetComponent<TilemapRenderer>().sortingOrder = order;
            }


            //set the scale of this object to 5% less pr difference in sideviewNumber
            var transform1 = transform;
            var scale = transform1.localScale;
            scale.x = scale.x - (xScale * sideviewDif);
            scale.y = scale.y - (yScale * sideviewDif);
            transform1.localScale = scale;


            //remove colliders for objects in other sideviews
            foreach (var o in objectsToTransform)
            {
                if (o.GetComponent<BoxCollider2D>() != null)
                {
                    o.GetComponent<BoxCollider2D>().enabled = false;
                }

                if (o.GetComponent<CircleCollider2D>() != null)
                {
                    o.GetComponent<CircleCollider2D>().enabled = false;
                }

                if (o.GetComponent<PolygonCollider2D>() != null)
                {
                    o.GetComponent<PolygonCollider2D>().enabled = false;
                }

                if (o.GetComponent<TilemapCollider2D>() != null)
                {
                    o.GetComponent<TilemapCollider2D>().enabled = false;
                    
                }
            }
        }
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
    
}