using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

//Smallest number runs earliest!

public class ETouchGeneralInput : MonoBehaviour
{
    #region Swipe Mechanic
    [SerializeField]
    private Vector2 _screenWidthBoundries = new Vector2(0, 0);

    [SerializeField]
    private Vector2 _screenHeightBoundries = new Vector2(0, 0);

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

    private Finger _movementFinger;
    private Vector2 _movementAmmount;
    #endregion

    [SerializeField]
    private Camera _mainCamera;

    [SerializeField]
    private ETouchPlayerMovement _eTouchPlayerMovement;

    [SerializeField]
    TrackingEveryInput _inputTracker = null;

    private void Awake()
    {
        _inputTracker = TrackingEveryInput.Instance;
        if( _inputTracker != null )
        {
            Debug.Log("Found Input Tracker");
        }
        else
        {
            Debug.Log("Nothing to Track");
        }
        _eTouchPlayerMovement = GetComponent<ETouchPlayerMovement>();
        _mainCamera = Camera.main;
        _screenHeightBoundries = new Vector2(0, _mainCamera.pixelHeight);
    }

    private void Start()
    {
        if (_eTouchPlayerMovement != null)
        {
            Debug.Log("Found Component" );
            if (_eTouchPlayerMovement._playerNumber == 1)
            {
                Debug.Log("Found Left Side");
                _screenWidthBoundries = _eTouchPlayerMovement.screenBoundryLeftSide;
            }
            else if (_eTouchPlayerMovement._playerNumber == 2)
            {
                Debug.Log("Found Right Side");
                _screenWidthBoundries = _eTouchPlayerMovement.screenBoundryRightSide;
            }
        }
        else
        {
            Debug.Log("Did Not Find Component");
            _screenWidthBoundries = new Vector2(0, _mainCamera.pixelWidth);
        }
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleGeneralFingerDown;
        ETouch.Touch.onFingerUp += HandleGeneralFingerLost;

    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleGeneralFingerDown;
        ETouch.Touch.onFingerUp -= HandleGeneralFingerLost;

        EnhancedTouchSupport.Disable();
    }

    private void HandleGeneralFingerDown(Finger touchedFinger)
    {
        
        if (_movementFinger == null)
        {
            //If you touched somewhere within the confines of the screen
            if(FingerScreenPositionWasWithinScreenBoundries(touchedFinger.screenPosition))
            {
                if(_eTouchPlayerMovement == null)
                {
                    _inputTracker.LogScreenPressDown(touchedFinger.screenPosition);
                }
                _movementFinger = touchedFinger;
                Vector3 position = Utils.ScreenCoordinatesTo2DWorldCoordinates(_mainCamera, touchedFinger.screenPosition);
                ETouch.Touch _currentTouch = touchedFinger.currentTouch;

                SwipeStart(position, (float)_currentTouch.startTime);
            }



        }

    }


    private void HandleGeneralFingerLost(Finger lostFinger)
    {

        if (_movementFinger == lostFinger)
        {
            if (_eTouchPlayerMovement == null)
            {
                _inputTracker.LogScreenLyftUp(lostFinger.screenPosition);
            }
            Vector3 lostPosition = Utils.ScreenCoordinatesTo2DWorldCoordinates(_mainCamera, lostFinger.screenPosition);
            ETouch.Touch _currentLostTouch = lostFinger.currentTouch;
            SwipeEnd(lostPosition, (float)_currentLostTouch.time);
        }
        
    }


    private bool FingerScreenPositionWasWithinScreenBoundries(Vector2 screenPosition)
    {
        if (screenPosition.x > _screenWidthBoundries.x
            && screenPosition.y > _screenHeightBoundries.x)
        {
            if (screenPosition.x < _screenWidthBoundries.y
                && screenPosition.y < _screenHeightBoundries.y)
            {
                return true;
            }
            return false;
        }
        return false;
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
            _trail.transform.position = Utils.ScreenCoordinatesTo2DWorldCoordinates(_mainCamera, _movementFinger.screenPosition);
            yield return null;
        }
    }

    private void SwipeEnd(Vector3 position, float time)
    {
        StopCoroutine(_coroutineForTrial);
        _movementFinger = null;
        _trail.SetActive(false);
        _endPosition = position;
        _endTime = time;
        //DetectSwipeDirection();
    }

    private void DetectSwipeDirection()
    {
        if (Vector3.Distance(_startPosition, _endPosition) >= _minimumDistance)
        {
            if ((_endTime - _startTime) <= _maximumTime)
            {
                Debug.DrawLine(_startPosition, _endPosition, Color.red, 5f);
                Vector3 direction = _endPosition - _startPosition;
                Vector2 directionTwoD = new Vector2(direction.x, direction.z).normalized;
                DebugTestSwipeDirection(directionTwoD);
            }
        }
    }

    private void DebugTestSwipeDirection(Vector2 directionTwoD)
    {
        if (Vector2.Dot(Vector2.up, directionTwoD) > directionThreshold)
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

}
