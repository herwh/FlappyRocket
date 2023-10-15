using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private InputController _inputController;
    [SerializeField] private float _moveForce;
    [SerializeField] private float _rotationForce;
    [SerializeField] private Rigidbody _rb;

    public event Action Moving;
    public event Action Stop;

    private bool _isMoving;

    private void Start()
    {
        _inputController.MoveButtonHold += MoveUp;
        _inputController.MoveButtonUp += StopMoving;
        _inputController.LeftButtonHold += RotateLeft;
        _inputController.RightButtonHold += RotateRight;
    }

    void FixedUpdate()
    {
        if (_isMoving)
        {
            _rb.AddRelativeForce(Vector3.up * (_moveForce * Time.deltaTime));
        }
    }

    private void MoveUp()
    {
        _isMoving = true;
        if (Moving != null) Moving();
    }

    private void RotateLeft()
    {
        _rb.freezeRotation = true;

        transform.Rotate(Vector3.forward * (_rotationForce * Time.deltaTime));

        _rb.freezeRotation = false;
    }

    private void RotateRight()
    {
        _rb.freezeRotation = true;

        transform.Rotate(Vector3.back * (_rotationForce * Time.deltaTime));

        _rb.freezeRotation = false;
    }

    private void StopMoving()
    {
        _isMoving = false;
        if (Stop != null) Stop();
    }

    private void OnDisable()
    {
        _inputController.MoveButtonHold -= MoveUp;
        _inputController.MoveButtonUp -= StopMoving;
        _inputController.LeftButtonHold -= RotateLeft;
        _inputController.RightButtonHold -= RotateRight;
    }
}