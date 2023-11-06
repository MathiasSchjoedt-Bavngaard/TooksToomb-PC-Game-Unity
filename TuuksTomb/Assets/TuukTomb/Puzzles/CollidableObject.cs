using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollidableObject : MonoBehaviour
{
    public new Collider2D collider;
    [SerializeField]
    private ContactFilter2D filter;
    private List<Collider2D> _collidedObjects = new (1); //Only one player
    protected virtual void Start()
    {
        if (!collider)
        {
            collider = GetComponent<Collider2D>();
        }
    }

    protected virtual void Update()
    {
        if (!collider)
        {
            return; // No collider assigned, so exit the method
        }
        
        _collidedObjects.Clear(); // Clear the list at the start of the update
        collider.OverlapCollider(filter, _collidedObjects);

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
