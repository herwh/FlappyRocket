using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float _levelLoadDelay;
    [SerializeField] private float _reloadDelay;
    [SerializeField] private AudioClip _crashSound;
    [SerializeField] private AudioClip _successSound;
    [SerializeField] private ParticleSystem _crashParticles;
    [SerializeField] private ParticleSystem _successParticles;

    private AudioSource _audioSource;
    private bool _isTransitioning;
    private bool _collisionDisabled;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _isTransitioning = false;
        _collisionDisabled = false;
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_isTransitioning || _collisionDisabled)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You collided with FRIENDLY object!");
                break;
            case "Fuel":
                Debug.Log("You got FUEL! Yay!");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartSuccessSequence() //todo extract to levelController
    {
        _isTransitioning = true;

        _audioSource.Stop();
        _audioSource.PlayOneShot(_successSound);

        _successParticles.transform.transform.parent = null;
        _successParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), _levelLoadDelay);
    }

    private void StartCrashSequence() //todo extract to levelController
    {
        _isTransitioning = true;

        _audioSource.Stop(); //todo extract to SFX
        _audioSource.PlayOneShot(_crashSound);

        _crashParticles.transform.parent = null;//todo extract to VFX
        _crashParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), _reloadDelay);
    }

    private void LoadNextLevel() //todo extract to levelController + Cheats
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex > SceneManager.sceneCount)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadLevel() //todo extract to levelController
    {
        var currenSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currenSceneIndex);
    }
    
    private void RespondToDebugKeys() //todo extract to Cheats
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            _collisionDisabled = !_collisionDisabled;
        }
    }
    
}
