using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class ETouchPlayerMovement : MonoBehaviour
{
    #region Joystick
    [SerializeField]
    public Vector2 screenBoundryLeftSide = new Vector2(0, 0);
    
    [SerializeField]
    public Vector2 screenBoundryRightSide = new Vector2(0, 0);


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
    private Camera _mainCamera;


    public int _playerNumber;

    private void Awake()
    {
        _mainCamera = Camera.main;
        screenBoundryLeftSide = new Vector2(0, _mainCamera.pixelWidth * 0.35f);
        screenBoundryRightSide = new Vector2(_mainCamera.pixelWidth * 0.65f, _mainCamera.pixelWidth);
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
            if( touchedFinger.index == 0 || touchedFinger.index == 1)
            {
                if (_playerNumber == 1)
                {
                    //If the finger falls between this range on the screen, we're going to move the left paddle
                    if ((touchedFinger.screenPosition.x >= screenBoundryLeftSide.x) && (touchedFinger.screenPosition.x <= screenBoundryLeftSide.y))
                    {
                        _movementFinger = touchedFinger;
                        _movementAmmount = Vector2.zero;
                        _leftJoystick.gameObject.SetActive(true);
                        _leftJoystick.RectTransform.sizeDelta = _JoystickSize;
                        _leftJoystick.RectTransform.anchoredPosition = ClampStartPosition(touchedFinger.screenPosition) - new Vector2(_JoystickSize.x / 2, _JoystickSize.y / 2);
                    }
                }


                if (_playerNumber == 2)
                {
                    //If the finger falls between this range on the screen, we're going to move the right paddle
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

        }
    }

    private void HandleFingerMove(Finger movedFinger)
    {
        if(movedFinger == _movementFinger)
        {
            Vector2 _knobPosition;
            float _maxMovement = _JoystickSize.x / 2;
            ETouch.Touch _currentTouch = movedFinger.currentTouch;

            if (_playerNumber == 1)
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

            if (_playerNumber == 2)
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
        if (_playerNumber == 1)
        {

            _movementFinger = null;
            _leftJoystick.Knob.anchoredPosition = Vector2.zero;
            _leftJoystick.gameObject.SetActive(false);


        }

        if (_playerNumber == 2)
        {

            _movementFinger = null;
            _rightJoystick.Knob.anchoredPosition = Vector2.zero;
            _rightJoystick.gameObject.SetActive(false);

        }
        _movementAmmount = Vector2.zero;
    }

    private Vector2 ClampStartPosition(Vector2 startPosition)
    {
        //Debug.Log($"Player touched at this coordinate point: {startPosition}, also screen wdith and height is {_mainCamera.pixelWidth}, {_mainCamera.pixelHeight}");
        //This clamps the position so the player can still see/move the joystick on screen

        #region Clamping the width
        if (startPosition.x < _JoystickSize.x/2 )
        {
            startPosition.x = _JoystickSize.x/2;
        }
        else if (startPosition.x > _mainCamera.pixelWidth - _JoystickSize.x / 2)
        {
            startPosition.x = _mainCamera.pixelWidth - _JoystickSize.x / 2;
        }
        #endregion

        #region Clamping the height
        if (startPosition.y < _JoystickSize.y/2)
        {
            startPosition.y = _JoystickSize.y/2;
        }
        else if (startPosition.y > _mainCamera.pixelHeight - _JoystickSize.y / 2)
        {
            startPosition.y = _mainCamera.pixelHeight - _JoystickSize.y / 2;
        }
        #endregion
        //Debug.Log($"But Joystick will start here {startPosition}, also screen wdith and height is {_mainCamera.pixelWidth}, {_mainCamera.pixelHeight}");
        return startPosition;
    }


    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleLostFinger;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();
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
