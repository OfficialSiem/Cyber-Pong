using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingTouchControls : MonoBehaviour
{
    private InputManager _inputManager;
    private PlayerInputActions _playerInputActions;
    private Camera _cameraMain;

    #region Swipe Mechanic
    [SerializeField]
    private Coroutine _coroutineForTrial;

    [SerializeField]
    private GameObject _trail;

    [SerializeField, Range(0.0f, 1.0f)]
    private float directionThreshold = 0.9f;

    [SerializeField]
    private float _minimumDistance = 0f;

    [SerializeField, Range(0.1f, 2.0f)]
    private float _maximumTime;

    [SerializeField]
    private Vector3 _startPosition;
    [SerializeField]
    private float _startTime;

    [SerializeField]
    private Vector3 _endPosition;
    [SerializeField]
    private float _endTime;

    #endregion

    private void Awake()
    {
        _inputManager = InputManager.Instance;
        _cameraMain = Camera.main;
        _playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _inputManager.OnStartTouch += MoveToWhereITouch;
        _inputManager.OnTouchPrimary += SwipeStart;
        _inputManager.OnEndTouchPrimary += SwipeEnd;
    }


    private void OnDisable()
    {
        _inputManager.OnEndTouch -= MoveToWhereITouch;
        _inputManager.OnEndTouchPrimary -= SwipeStart;
        _inputManager.OnEndTouchPrimary -= SwipeEnd;
    }


    private void SwipeStart(Vector3 position, float time)
    {
        _startPosition = position;
        _startTime = time;
        _trail.SetActive(true);
        _trail.transform.position = position;
        _coroutineForTrial = StartCoroutine(Trial());
    }

    private IEnumerator Trial()
    {
        while (true)
        {
            _trail.transform.position = _inputManager.PrimaryPosition();
            yield return null;
        }
    }

    private void SwipeEnd(Vector3 position, float time)
    {
        StopCoroutine(_coroutineForTrial);
        _trail.SetActive(false);
        _endPosition = position;
        _endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if(Vector3.Distance(_startPosition, _endPosition) >= _minimumDistance)
        {
            if( (_endTime - _startTime) <= _maximumTime)
            {
                Debug.DrawLine(_startPosition, _endPosition, Color.red, 5f);
                Vector3 direction = _endPosition - _startPosition;
                Vector2 directionTwoD = new Vector2(direction.x, direction.z).normalized;
                SwipeDirection(directionTwoD);
            }
        }
    }

    private void SwipeDirection(Vector2 directionTwoD)
    {
        if(Vector2.Dot(Vector2.up, directionTwoD) > directionThreshold)
        {
            Debug.Log("Swipe Up");
        }
        else if (Vector2.Dot(Vector2.down, directionTwoD) > directionThreshold)
        {
            Debug.Log("Swipe Down");
        }
        else if (Vector2.Dot(Vector2.left, directionTwoD) > directionThreshold)
        {
            Debug.Log("Swipe Left");
        }
        else if (Vector2.Dot(Vector2.right, directionTwoD) > directionThreshold)
        {
            Debug.Log("Swipe Right");
        }
    }

    public void MoveToWhereITouch(Vector2 screenPosition, float time)
    {
             
            //Debug.Log($"Pixel Coordinate the player pointed: {screenPosition}");

        
            Vector3 screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, _cameraMain.nearClipPlane);
            //Debug.Log($"Screen Coordinate player pointed: {screenCoordinates}");
            Vector3 worldCoordinates = _cameraMain.ScreenToWorldPoint(screenCoordinates);
            //Debug.Log($"In-Game World Coordinate player pointed too: {worldCoordinates}");
            worldCoordinates.y = 0;
            transform.position = worldCoordinates;
        

    }


}
