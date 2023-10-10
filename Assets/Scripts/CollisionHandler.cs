using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float _levelLoadDelay;
    [SerializeField] private float _reloadDelay;
    [SerializeField] private AudioClip _crashSound;
    [SerializeField] private AudioClip _successSound;

    private AudioSource _audioSource;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
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
        _audioSource.PlayOneShot(_successSound);
        
        //todo add VFX upon finish
        
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel),_levelLoadDelay);
    }
    
    private void StartCrashSequence()
    {
        _audioSource.PlayOneShot(_crashSound);
        
        //todo add VFX upon crash
        
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
