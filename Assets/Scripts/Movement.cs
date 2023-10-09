using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveForce;
    [SerializeField] private float _rotationForce;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ProcessInput();
        ProcessRotation();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            MoveUp();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateToDirection(Vector3.forward);

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateToDirection(Vector3.back);
        }
    }

    private void MoveUp()
    {
        _rb.AddRelativeForce(Vector3.up * (_moveForce * Time.deltaTime));
    }

    private void RotateToDirection(Vector3 direction)
    {
        _rb.freezeRotation = true;
        transform.Rotate(direction * (_rotationForce * Time.deltaTime));
        _rb.freezeRotation = false;
    }
}
