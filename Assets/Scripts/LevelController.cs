using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private float _levelLoadDelay;
    [SerializeField] private float _reloadDelay;
    [SerializeField] private AudioClip _crashSound;
    [SerializeField] private AudioClip _successSound;
    [SerializeField] private ParticleSystem _crashParticles;
    [SerializeField] private ParticleSystem _successParticles;

    private bool _isTransitioning;
    private AudioSource _audioSource;

    private void Start()
    {
        _collisionHandler.Collided += CollisionHandle;
        
        _audioSource = GetComponent<AudioSource>();
        _isTransitioning = false;
    }

    private void CollisionHandle(Collision other)
    {
        if (_isTransitioning)
            return;

        switch (other.gameObject.tag)
        {
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

        _audioSource.Stop(); //todo extract to SFX
        _audioSource.PlayOneShot(_crashSound);

        _crashParticles.transform.parent = null; //todo extract to VFX
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

    private void ReloadLevel()
    {
        var currenSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currenSceneIndex);
    }

    private void OnDisable()
    {
        _collisionHandler.Collided -= CollisionHandle;
    }
}