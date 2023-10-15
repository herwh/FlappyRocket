using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private InputController _inputController;
    [SerializeField] private float _levelLoadDelay;
    [SerializeField] private float _reloadDelay;
    
    public event Action Success;
    public event Action Crash;

    private bool _isTransitioning;

    private void Start()
    {
        _collisionHandler.Collided += CollisionHandle;
        _inputController.NextLevelButtonDown += LoadNextLevel;

        _isTransitioning = false;
    }

    private void CollisionHandle(Collision other)
    {
        if (_isTransitioning)
            return;

        switch (other.gameObject.tag)
        {
            case "Friendly" :
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

        if (Success != null) Success();

        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), _levelLoadDelay);
    }

    private void StartCrashSequence()
    {
        _isTransitioning = true;

        if (Crash != null) Crash();

        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), _reloadDelay);
    }

    private void LoadNextLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
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
        _inputController.NextLevelButtonDown -= LoadNextLevel;
    }
}