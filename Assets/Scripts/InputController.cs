using UnityEngine;

public class InputController
    {
        
        private void Update()
        {
            RespondToDebugKeys();
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