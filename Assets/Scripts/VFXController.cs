 using UnityEngine;

 public class VFXController : MonoBehaviour
 {
     [SerializeField] private Movement _movement;
     [SerializeField] private ParticleSystem _flyTrail;

     private void Start()
     {
         _movement.Moving += PlayFlyTrail;
         _movement.Stop += StopFlyTrail;
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

     private void OnDisable()
     {
         _movement.Moving -= PlayFlyTrail;
         _movement.Stop -= StopFlyTrail;
     }
 }