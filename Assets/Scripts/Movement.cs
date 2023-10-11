using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveForce;
    [SerializeField] private float _rotationForce;
    [SerializeField] private AudioClip _flySound;
    [SerializeField] private ParticleSystem _boosterTrail;
    
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
            

            if (!_audioSource.isPlaying) //todo refactor (check rocketIsFlying)
            {
                _audioSource.PlayOneShot(_flySound);
            }

            if (!_boosterTrail.isPlaying)
            {
                _boosterTrail.Play();
            }
        }

        else //todo refactor (check rocketIsFlying)
        {
            _audioSource.Stop();
            _boosterTrail.Stop();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            RotateToDirection(Vector3.forward);

        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
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
