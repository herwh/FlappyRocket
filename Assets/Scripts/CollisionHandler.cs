using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private InputController _inputController;
    public event Action<Collision> Collided;

    private bool _collisionDisabled;

    private void Start()
    {
        _collisionDisabled = false;
        _inputController.SwitchCollisionStateButtonDown += SwitchCollisionState;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if ( _collisionDisabled)
        {
            return;
        }

        if (Collided != null) Collided(other);
    }

    private void SwitchCollisionState()
    {
        _collisionDisabled = !_collisionDisabled;
    }

    private void OnDisable()
    {
        _inputController.SwitchCollisionStateButtonDown -= SwitchCollisionState;
    }
}
