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
        ProcessMove();
        ProcessRotation();
    }

    private void ProcessMove()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            MoveUp();
        }

        else
        {
            StopMoving();
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

        if (!_audioSource.isPlaying)//todo extract to SFX
        {
            _audioSource.PlayOneShot(_flySound);
        }

        if (!_boosterTrail.isPlaying)//todo extract to VFX
        {
            _boosterTrail.Play();
        }
    }

    private void RotateToDirection(Vector3 direction)
    {
        _rb.freezeRotation = true;
        
        transform.Rotate(direction * (_rotationForce * Time.deltaTime));
        
        _rb.freezeRotation = false;
    }
    
    private void StopMoving()
    {
        _audioSource.Stop();//todo extract to SFX
        _boosterTrail.Stop();//todo extract to VFX
    }
}
