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

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _isTransitioning = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_isTransitioning)
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

    private void StartSuccessSequence()
    {
        _isTransitioning = true;

        _audioSource.Stop();
        _audioSource.PlayOneShot(_successSound);

        _successParticles.transform.transform.parent = null;
        _successParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), _levelLoadDelay);
    }

    private void StartCrashSequence()
    {
        _isTransitioning = true;

        _audioSource.Stop();
        _audioSource.PlayOneShot(_crashSound);

        _crashParticles.transform.parent = null;
        _crashParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), _reloadDelay);
    }

    private void LoadNextLevel()
    {
        var currenSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var nextSceneIndex = currenSceneIndex + 1;

        if (nextSceneIndex > SceneManager.sceneCount)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadLevel()
    {
        var currenSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currenSceneIndex);
    }
}
