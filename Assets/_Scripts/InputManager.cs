using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

//Smallest number runs earliest!
[DefaultExecutionOrder(-10)]
public class InputManager : Singleton<InputManager>
{
    private PlayerInputActions _playerInputActions;
    private Camera mainCamera;

    #region Events

    //Testing out Swiping Mechanics
    public delegate void StartTouchPrimaryEvent(Vector3 position, float time);
    public event StartTouchPrimaryEvent OnTouchPrimary;

    public delegate void EndTouchPrimaryEvent(Vector3 position, float time);
    public event EndTouchPrimaryEvent OnEndTouchPrimary;


    //Getting a point on screen
    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;

    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;
    #endregion

    // Start is called before the first frame update
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _playerInputActions.Enable();
    }

    private void OnDisable()
    {
        _playerInputActions.Disable();
    }

    private void Start()
    {
        //Always tell when the action starts what to do, and when the action is cancelled what to do
        _playerInputActions.SwipeControlTest.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        _playerInputActions.SwipeControlTest.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
        _playerInputActions.TouchControlTest.TouchPress.started += ctx => StartTouch(ctx);
        _playerInputActions.TouchControlTest.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if(OnTouchPrimary != null)
        {
            //Debug.Log("Start Touch" + _playerInputActions.SwipeControlTest.PrimaryPosition.ReadValue<Vector2>());
            Vector3 position = Utils.ScreenToTwoDWorld(mainCamera, _playerInputActions.SwipeControlTest.PrimaryPosition.ReadValue<Vector2>());
            OnTouchPrimary(position, (float)context.startTime);
        }
    }


    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if(OnEndTouchPrimary != null)
        {
            //Debug.Log("End Touch" + _playerInputActions.SwipeControlTest.PrimaryPosition.ReadValue<Vector2>());
            Vector3 position = Utils.ScreenToTwoDWorld(mainCamera, _playerInputActions.SwipeControlTest.PrimaryPosition.ReadValue<Vector2>());
            OnEndTouchPrimary(position, (float)context.time);
        }
    }

    public Vector3 PrimaryPosition()
    {
        return Utils.ScreenToTwoDWorld(mainCamera, _playerInputActions.SwipeControlTest.PrimaryPosition.ReadValue<Vector2>());
    }



    private void StartTouch(InputAction.CallbackContext context)
    {
        Vector2 touchPosition = _playerInputActions.TouchControlTest.TouchPosition.ReadValue<Vector2>();
        //Debug.Log("Touch Started " + touchPosition);
        if(OnStartTouch != null)
        {
            OnStartTouch(touchPosition, (float)context.startTime);
        }
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        Vector2 touchPosition = _playerInputActions.TouchControlTest.TouchPosition.ReadValue<Vector2>();
        //Debug.Log("Touch Ended " + touchPosition);
        if (OnEndTouch != null)
        {
            OnEndTouch(touchPosition, (float)context.time);
        }
    }



}
