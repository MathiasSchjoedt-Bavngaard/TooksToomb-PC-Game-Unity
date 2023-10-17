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
        color.r = color.r - (colorScale * sceneSideviewNumber);
        color.g = color.g - (colorScale * sceneSideviewNumber);
        color.b = color.b - (colorScale * sceneSideviewNumber);
        GetComponent<SpriteRenderer>().color = color;
        
        //make color of childrend darker for each difference in sideviewNumber
foreach (Transform child in transform)
{
    var childColor = child.GetComponent<SpriteRenderer>().color;
    childColor.r = childColor.r - (colorScale * sceneSideviewNumber);
    childColor.g = childColor.g - (colorScale * sceneSideviewNumber);
    childColor.b = childColor.b - (colorScale * sceneSideviewNumber);
    child.GetComponent<SpriteRenderer>().color = childColor;
}
    }

    
}
