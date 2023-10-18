using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HieroglyphInPlace : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Hieroglyph")) return;
        PlayerPrefs.SetFloat("HieroglyphInPlace", 1);
       
    }
    
    
}
