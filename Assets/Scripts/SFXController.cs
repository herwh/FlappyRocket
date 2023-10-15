using UnityEngine;

public class SFXController : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private LevelController _levelController;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _flySound;
    [SerializeField] private AudioClip _crashSound;
    [SerializeField] private AudioClip _successSound;


    private void Start()
    {
        _movement.Moving += PlayFlySound;
        _movement.Stop += StopSound;
        _levelController.Success += PlaySuccessSound;
        _levelController.Crash += PlayCrashSound;
    }

    private void PlayFlySound()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(_flySound);
        }
    }

    private void PlaySuccessSound()
    {
        StopSound();
        _audioSource.PlayOneShot(_successSound);
    }

    private void PlayCrashSound()
    {
        StopSound();
        _audioSource.PlayOneShot(_crashSound);
    }

    private void StopSound()
    {
        _audioSource.Stop();
    }

    private void OnDisable()
    {
        _movement.Moving -= PlayFlySound;
        _movement.Stop -= StopSound;
        _levelController.Success -= PlaySuccessSound;
        _levelController.Crash -= PlayCrashSound;
    }
}