using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using InControl;

public class HandControls : MonoBehaviour
{
    [SerializeField]
    private Animator handAnimator;

    [SerializeField]
    private Transform hand;

    [SerializeField]
    private Rigidbody handBody;

    #region keybord controls keys
    [SerializeField]
    private KeyCode grabKey;

    [SerializeField]
    private KeyCode rudeKey;

    [SerializeField]
    private KeyCode metalKey;

    [SerializeField]
    private KeyCode moveLeftKey;

    [SerializeField]
    private KeyCode moveRightKey;

    [SerializeField]
    private KeyCode moveUpKey;

    [SerializeField]
    private KeyCode moveDownKey;

    [SerializeField]
    private KeyCode moveForwardKey;

    [SerializeField]
    private KeyCode moveBackwardKey;

    [SerializeField]
    private KeyCode rotateHorizontalKey;

    [SerializeField]
    private KeyCode rotateVerticalKey;

    [SerializeField]
    private KeyCode rotateForwardKey;
    #endregion


    #region cnotroller controls keys
    [SerializeField]
    private InputControlType controllerGrabKey;

    [SerializeField]
    private InputControlType controllerAlternativeGrabKey;

    [SerializeField]
    private InputControlType controllerRudeKey;

    [SerializeField]
    private InputControlType controllerMetalKey;

    [SerializeField]
    private InputControlType controllerMoveXAxisKey;

    [SerializeField]
    private InputControlType controllerMoveYAxisKey;

    [SerializeField]
    private InputControlType controllerMoveZAxisKey;

    [SerializeField]
    private InputControlType controllerRotateAxisKey;
    #endregion

    [SerializeField]
    private float moveSpeed = 0.25f;

    [SerializeField]
    private float rotateSpeed = 50f;

    [SerializeField]
    private bool reverseRotate;

    #region events
    [SerializeField]
    private UnityEvent OnBeginGrab;

    [SerializeField]
    private UnityEvent OnEndGrab;
    #endregion


    private bool WasPressed(InputControlType controlType)
    {
        return InputManager.ActiveDevice.GetControl(controlType).WasPressed;
    }

    private bool WasReleased(InputControlType controlType)
    {
        return InputManager.ActiveDevice.GetControl(controlType).WasReleased;
    }

    private void CheckGesture()
    {
        if (Input.GetKeyDown(grabKey) || WasPressed(controllerGrabKey) || WasPressed(controllerAlternativeGrabKey))
        {
            handAnimator.SetBool("Grab", true);
            OnBeginGrab.Invoke();
        }

        if (Input.GetKeyUp(grabKey) || WasReleased(controllerGrabKey) || WasReleased(controllerAlternativeGrabKey))
        {
            handAnimator.SetBool("Grab", false);
            OnEndGrab.Invoke();
        }

        if (Input.GetKeyDown(rudeKey) || WasPressed(controllerRudeKey))
        {
            handAnimator.SetBool("Rude", true);
        }

        if (Input.GetKeyUp(rudeKey) || WasReleased(controllerRudeKey))
        {
            handAnimator.SetBool("Rude", false);
        }

        if (Input.GetKeyDown(metalKey) || WasPressed(controllerMetalKey))
        {
            handAnimator.SetBool("Metal", true);
        }

        if (Input.GetKeyUp(metalKey) || WasReleased(controllerMetalKey))
        {
            handAnimator.SetBool("Metal", false);
        }
    }

    private void Move(Vector3 direction)
    {
        hand.Translate(direction * Time.deltaTime * moveSpeed, Space.World);
    }

    private void MoveLeft()
    {
        Move(Vector3.back);
    }

    private void MoveRight()
    {
        Move(Vector3.forward);
    }

    private void MoveUp()
    {
        Move(Vector3.up);
    }

    private void MoveDown()
    {
        Move(Vector3.down);
    }

    private void MoveForward()
    {
        Move(Vector3.left);
    }

    private void MoveBackward()
    {
        Move(Vector3.right);
    }

    private void CheckMove()
    {
        if (Input.GetKey(moveLeftKey))
        {
            MoveLeft();
        }
        else if (Input.GetKey(moveRightKey))
        {
            MoveRight();
        }

        if (Input.GetKey(moveUpKey))
        {
            MoveUp();
        }
        else if (Input.GetKey(moveDownKey))
        {
            MoveDown();
        }

        if (Input.GetKey(moveForwardKey))
        {
            MoveForward();
        }
        else if (Input.GetKey(moveBackwardKey))
        {
            MoveBackward();
        }
    }

    private void CheckControllerMove()
    {
        var horizontalMove = InputManager.ActiveDevice.GetControl(controllerMoveXAxisKey).Value;
        var verticalMove = InputManager.ActiveDevice.GetControl(controllerMoveYAxisKey).Value;
        var depthMove = InputManager.ActiveDevice.GetControl(controllerMoveZAxisKey).Value;

        handBody.velocity = new Vector3(
            -depthMove * moveSpeed,
            verticalMove * moveSpeed,
            horizontalMove * moveSpeed
        );
    }

    private void Rotate(Vector3 direction)
    {
        hand.Rotate(direction * Time.deltaTime * rotateSpeed);
    }

    private void RotateHorizontalLeft()
    {
        Rotate(new Vector3(0, -1, 0));
    }

    private void RotateHorizontalRight()
    {
        Rotate(new Vector3(0, 1, 0));
    }

    private void RotateVerticalLeft()
    {
        Rotate(new Vector3(0, 0, -1));
    }

    private void RotateVerticalRight()
    {
        Rotate(new Vector3(0, 0, 1));
    }

    private void RotateForward()
    {
        Rotate(new Vector3(1, 0, 0));
    }

    private void RotateBackward()
    {
        Rotate(new Vector3(-1, 0, 0));
    }

    private void CheckRotate()
    {
        var shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (shift && Input.GetKey(rotateHorizontalKey))
        {
            if (reverseRotate)
            {
                RotateHorizontalRight();
            }
            else
            {
                RotateHorizontalLeft();   
            }
        }
        else if (Input.GetKey(rotateHorizontalKey))
        {
            if (reverseRotate)
            {
                RotateHorizontalLeft();
            }
            else
            {
                RotateHorizontalRight();
            }
        }

        if (shift && Input.GetKey(rotateVerticalKey))
        {
            if (reverseRotate)
            {
                RotateHorizontalRight();
            }
            else
            {
                RotateVerticalLeft();
            }
        }
        else if (Input.GetKey(rotateVerticalKey))
        {
            if (reverseRotate)
            {
                RotateVerticalLeft();
            }
            else
            {
                RotateVerticalRight();
            }
        }

        if (shift && Input.GetKey(rotateForwardKey))
        {
            if (reverseRotate)
            {
                RotateForward();
            }
            else
            {
                RotateBackward();
            }
        }
        else if (Input.GetKey(rotateForwardKey))
        {
            if (reverseRotate)
            {
                RotateBackward();
            }
            else
            {
                RotateForward();
            }
        }
    }

    private void CheckControllerRotate()
    {
        var rotate = InputManager.ActiveDevice.GetControl(controllerRotateAxisKey).Value;
        handBody.angularVelocity = new Vector3(rotate, 0, 0);
    }

    private void Update()
    {
        CheckGesture();
        //CheckMove();
        //CheckRotate();
        CheckControllerMove();
        CheckControllerRotate();
    }
}
