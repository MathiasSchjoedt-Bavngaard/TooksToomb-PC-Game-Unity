using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidableObject : MonoBehaviour
{
    public Collider2D z_Collider;
    [SerializeField]
    private ContactFilter2D z_Filter;
    private List<Collider2D> z_CollidedObjects = new (1); //Only one player
    private void Start()
    {
        
    }

    private void Update()
    {
        z_Collider.OverlapCollider(z_Filter, z_CollidedObjects);
        foreach (var o in z_CollidedObjects)
        {
            Debug.Log("Collided with " + o.name);
        }
    }
}
