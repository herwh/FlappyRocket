using UnityEngine;

public class SFXController : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private AudioClip _flySound;
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        _movement.Moving += PlayFlySound;
        _movement.Stop += StopFlySound;
    }

    private void PlayFlySound()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(_flySound);
        }
    }

    private void StopFlySound()
    {
        _audioSource.Stop();
    }

    private void OnDisable()
    {
        _movement.Moving -= PlayFlySound;
        _movement.Stop -= StopFlySound;
    }
}