using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action<Collision> Collided;

    private bool _collisionDisabled;

    private void Start()
    {
        _collisionDisabled = false;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if ( _collisionDisabled)
        {
            return;
        }

        if (Collided != null) Collided(other);
    }
}
