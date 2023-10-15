using System;
using UnityEngine;

public class InputController: MonoBehaviour
{
    public event Action MoveButtonHold;
    public event Action MoveButtonUp;
    public event Action LeftButtonHold;
    public event Action RightButtonHold;
        
    private void Update()
    {
        RespondToDebugKeys();
        MoveButtonHandler();
        RotationButtonsHandler();
    }

    private void MoveButtonHandler()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (MoveButtonHold != null) MoveButtonHold();
        }

        else
        {
            if (MoveButtonUp != null) MoveButtonUp();
        }
    }

    private void RotationButtonsHandler()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (LeftButtonHold != null) LeftButtonHold();
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (RightButtonHold != null) RightButtonHold();
        }
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