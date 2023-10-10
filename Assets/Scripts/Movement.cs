using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveForce;
    [SerializeField] private float _rotationForce;
    
    [SerializeField] private AudioClip _flySound;
    
    private Rigidbody _rb;
    private AudioSource _audioSource;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
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

            if (!_audioSource.isPlaying)
            {
                _audioSource.PlayOneShot(_flySound);
            }
        }

        else
        {
            _audioSource.Stop();
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
