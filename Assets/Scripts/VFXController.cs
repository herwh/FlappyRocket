 using UnityEngine;

 public class VFXController : MonoBehaviour
 {
     [SerializeField] private Movement _movement;
     [SerializeField] private LevelController _levelController;
     [SerializeField] private ParticleSystem _flyTrail;
     [SerializeField] private ParticleSystem _crashParticles;
     [SerializeField] private ParticleSystem _successParticles;

     private void Start()
     {
         _movement.Moving += PlayFlyTrail;
         _movement.Stop += StopFlyTrail;
         _levelController.Success += PlaySuccessParticles;
         _levelController.Crash += PlayCrashParticles;
     }

     private void PlayFlyTrail()
     {
         if (!_flyTrail.isPlaying)
         {
             _flyTrail.Play();
         }
     }

     private void StopFlyTrail()
     {
         _flyTrail.Stop();
     }
     
     private void PlaySuccessParticles()
     {
         _successParticles.transform.parent = null;
         _successParticles.Play();
     }
     
     private void PlayCrashParticles()
     {
         _crashParticles.transform.parent = null;
         _crashParticles.Play();
     }

     private void OnDisable()
     {
         _movement.Moving -= PlayFlyTrail;
         _movement.Stop -= StopFlyTrail;
         _levelController.Success -= PlaySuccessParticles;
         _levelController.Crash -= PlayCrashParticles;
     }
 }