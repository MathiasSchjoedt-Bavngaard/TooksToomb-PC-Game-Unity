using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class CollidableObject : MonoBehaviour
{
    
    private Collider2D _collider;
    [SerializeField]
    private ContactFilter2D filter;
    private List<Collider2D> _collidedObjects = new (1); //Only one player
    protected virtual void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        _collider.OverlapCollider(filter, _collidedObjects);
        foreach (var o in _collidedObjects)
        {
            WhenCollided(o.GameObject());
        }
    }

    protected virtual void WhenCollided(GameObject collidedObj)
    {
        Debug.Log("Collided with " + collidedObj.name);
    }
}
