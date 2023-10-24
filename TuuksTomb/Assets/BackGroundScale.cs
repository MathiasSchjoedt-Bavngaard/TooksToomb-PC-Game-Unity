using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScale : MonoBehaviour
{
    
    public float sceneSideviewNumber;
    public float xScale = 0.05f;
    public float yScale = 0.05f;
     private float colorScale = 0.08f;
    
    // Start is called before the first frame update
    void Start()
    {
        
        //set the scale of this object to 5% less pr difference in sideviewNumber
        var scale = transform.localScale;
        scale.x = scale.x - (xScale * sceneSideviewNumber);
        scale.y = scale.y - (yScale * sceneSideviewNumber);
        
        transform.localScale = scale;
        
        //transform order in layer to 1 less pr difference in sideviewNumber
        var order = GetComponent<SpriteRenderer>().sortingOrder;
        order = (int)(order - sceneSideviewNumber);
        GetComponent<SpriteRenderer>().sortingOrder = order;
        
        //make background darker for each difference in sideviewNumber
        var color = GetComponent<SpriteRenderer>().color;
  
        GetComponent<SpriteRenderer>().color = ScaledownColor(color, sceneSideviewNumber);
        
        //make color of childrend darker for each difference in sideviewNumber
foreach (Transform child in transform)
{
    var childColor = child.GetComponent<SpriteRenderer>().color;
    child.GetComponent<SpriteRenderer>().color = ScaledownColor( childColor, sceneSideviewNumber);
}
    }
    private Color ScaledownColor(Color color, float sideviewDif)
    {
        var procents = (colorScale * sideviewDif);
        
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
