using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerTouchMovement : MonoBehaviour
{
    #region Joystick
    [SerializeField]
    private Vector2 screenBoundryLeftSide = new Vector2(0, 0);
    
    [SerializeField]
    private Vector2 screenBoundryRightSide = new Vector2(0, 0);


    [SerializeField]
    private Vector2 _JoystickSize = new Vector2(300, 300);

    [SerializeField]
    private FloatingJoystick _leftJoystick;

    [SerializeField]
    private FloatingJoystick _rightJoystick;

    private Finger _movementFinger;
    private Vector2 _movementAmmount;
    #endregion

    #region Paddle
    [SerializeField]
    private float paddleSpeed = 5.0f;

    [SerializeField]
    private Rigidbody paddlesRigidbody;
    #endregion

    [SerializeField]
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        screenBoundryLeftSide = new Vector2(0, mainCamera.pixelWidth * 0.35f);
        screenBoundryRightSide = new Vector2(mainCamera.pixelWidth * 0.65f, mainCamera.pixelWidth);
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleLostFinger;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }

    private void HandleFingerDown(Finger touchedFinger)
    {
        if(_movementFinger == null)
        {
            //If the finger falls between this range on the screen, we're going to move the left paddle
            if((touchedFinger.screenPosition.x >= screenBoundryLeftSide.x) && (touchedFinger.screenPosition.x <= screenBoundryLeftSide.y))
            {
                _movementFinger = touchedFinger;
                _movementAmmount = Vector2.zero;
                _leftJoystick.gameObject.SetActive(true);
                _leftJoystick.RectTransform.sizeDelta = _JoystickSize;
                _leftJoystick.RectTransform.anchoredPosition = ClampStartPosition(touchedFinger.screenPosition) - new Vector2(_JoystickSize.x / 2, _JoystickSize.y / 2);
            }

            if ((touchedFinger.screenPosition.x >= screenBoundryRightSide.x) && (touchedFinger.screenPosition.x <= screenBoundryRightSide.y))
            {
                _movementFinger = touchedFinger;
                _movementAmmount = Vector2.zero;
                _rightJoystick.gameObject.SetActive(true);
                _rightJoystick.RectTransform.sizeDelta = _JoystickSize;
                _rightJoystick.RectTransform.anchoredPosition = ClampStartPosition(touchedFinger.screenPosition) - new Vector2(_JoystickSize.x / 2, _JoystickSize.y / 2);
            }
        }
    }

    private void HandleFingerMove(Finger movedFinger)
    {
        if(movedFinger == _movementFinger)
        {
            Vector2 _knobPosition;
            float _maxMovement = _JoystickSize.x / 2;
            ETouch.Touch _currentTouch = movedFinger.currentTouch;

            if ((movedFinger.screenPosition.x >= screenBoundryLeftSide.x) && (movedFinger.screenPosition.x <= screenBoundryLeftSide.y))
            {
                Vector2 _shiftedJoyStickCenter = _leftJoystick.RectTransform.anchoredPosition;
                Vector2 _trueJoystickCenter = _leftJoystick.RectTransform.anchoredPosition + new Vector2(_JoystickSize.x / 2, _JoystickSize.y / 2);

                //if the finger is moving inside a certain range from the joystick
                if (Vector2.Distance(_currentTouch.screenPosition, _trueJoystickCenter) > _maxMovement)
                {
                    _knobPosition = (_currentTouch.screenPosition - _trueJoystickCenter).normalized * _maxMovement;
                }
                else
                {
                    _knobPosition = _currentTouch.screenPosition - _trueJoystickCenter;
                }

                //Anchor the knob icon to this normalized value
                _leftJoystick.Knob.anchoredPosition = _knobPosition;

                //set the movement ammount here
                _movementAmmount = _knobPosition / _maxMovement;
            }

            if ((movedFinger.screenPosition.x >= screenBoundryRightSide.x) && (movedFinger.screenPosition.x <= screenBoundryRightSide.y))
            {
                Vector2 _shiftedJoyStickCenter = _rightJoystick.RectTransform.anchoredPosition;
                Vector2 _trueJoystickCenter = _rightJoystick.RectTransform.anchoredPosition + new Vector2(_JoystickSize.x / 2, _JoystickSize.y / 2);

                //if the finger is moving inside a certain range from the joystick
                if (Vector2.Distance(_currentTouch.screenPosition, _trueJoystickCenter) > _maxMovement)
                {
                    _knobPosition = (_currentTouch.screenPosition - _trueJoystickCenter).normalized * _maxMovement;
                }
                else
                {
                    _knobPosition = _currentTouch.screenPosition - _trueJoystickCenter;
                }

                //Anchor the knob icon to this normalized value
                _rightJoystick.Knob.anchoredPosition = _knobPosition;

                //set the movement ammount here
                _movementAmmount = _knobPosition / _maxMovement;
            }

        }
    }

    private void HandleLostFinger(Finger lostFinger)
    {

        if ((lostFinger.screenPosition.x >= screenBoundryLeftSide.x) && (lostFinger.screenPosition.x <= screenBoundryLeftSide.y))
        {
            _movementFinger = null;
            _leftJoystick.Knob.anchoredPosition = Vector2.zero;
            _leftJoystick.gameObject.SetActive(false);
            
        }

        if ((lostFinger.screenPosition.x >= screenBoundryRightSide.x) && (lostFinger.screenPosition.x <= screenBoundryRightSide.y))
        {
            _movementFinger = null;
            _rightJoystick.Knob.anchoredPosition = Vector2.zero;
            _rightJoystick.gameObject.SetActive(false);

        }
        _movementAmmount = Vector2.zero;
    }

    private Vector2 ClampStartPosition(Vector2 startPosition)
    {
        Debug.Log($"Player touched at this coordinate point: {startPosition}, also screen wdith and height is {mainCamera.pixelWidth}, {mainCamera.pixelHeight}");
        //This clamps the position so the player can still see/move the joystick on screen

        #region Clamping the width
        if (startPosition.x < _JoystickSize.x/2 )
        {
            startPosition.x = _JoystickSize.x/2;
        }
        else if (startPosition.x > mainCamera.pixelWidth - _JoystickSize.x / 2)
        {
            startPosition.x = mainCamera.pixelWidth - _JoystickSize.x / 2;
        }
        #endregion

        #region Clamping the height
        if (startPosition.y < _JoystickSize.y/2)
        {
            startPosition.y = _JoystickSize.y/2;
        }
        else if (startPosition.y > mainCamera.pixelHeight - _JoystickSize.y / 2)
        {
            startPosition.y = mainCamera.pixelHeight - _JoystickSize.y / 2;
        }
        #endregion
        Debug.Log($"But Joystick will start here {startPosition}, also screen wdith and height is {mainCamera.pixelWidth}, {mainCamera.pixelHeight}");
        return startPosition;
    }


    private void OnDisable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleLostFinger;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
    }

    private void Update()
    {
        //If theres an action to be called
        if (_movementAmmount.x != 0.0f)
        {
            MovePaddle();
        }
    }

    void MovePaddle()
    {
        Vector3 paddleVelocity = paddlesRigidbody.velocity;
        paddleVelocity.x = _movementAmmount.x * paddleSpeed * paddleSpeed;
        paddlesRigidbody.velocity = paddleVelocity;
    }
}
