using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Is this Player 1, 2, 3, 4.. etc?")]
    [SerializeField] public int playerNumber = 1;



    #region Paddle
    [SerializeField]
    private GameObject _paddle = null;
    private Rigidbody _paddlesRigidbody = null;
    private Vector2 _paddlesVelocity = Vector2.zero;

    [SerializeField]
    private float _paddleSpeedModifier = 0.0f;

    float _horizontalMovement = 0.0f;
    #endregion

    #region Private Player Attributes
    //Controller for Left/Right Player movement (Joystick, button clicks, etc [determined in Player settings])
    private InputAction _inputAction = null;
    private PlayerInput _playerInput = null;
    private PlayerInputActions _playerInputActions = null;
    #endregion 

    private void OnEnable()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInputActions = new PlayerInputActions();
        //Enable Player Action map
        _playerInputActions.UI.Disable();
        _playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        //Disable Player Action map
        _playerInputActions.Player.Disable();
        _paddle = null;
        _paddlesRigidbody = null;
        _paddleSpeedModifier = 0.0f;
        _playerInputActions.Player.Disable();
        _playerInputActions.UI.Enable();
        _playerInputActions = null;
    }

    private void Start()
    {
        Debug.Log("Going to find paddles");
        if (playerNumber == 1)
        {
            _paddle = GameObject.Find("Scene/PongGame/paddle_blue");

        }
        else if (playerNumber == 2)
        {
            _paddle = GameObject.Find("Scene/PongGame/paddle_green");
        }
        if(_paddle == null)
        {
            Debug.Log("COULDNT FIND PADDLES");
        }
        else{
            _paddlesRigidbody = _paddle.GetComponent<Rigidbody>();
            var _paddleComponent = _paddle.GetComponent<Paddle>();
            if (_paddleComponent != null)
            {
                _paddleSpeedModifier = _paddleComponent.GetPaddleBaseSpeed();
            }
            GetHorizontalMovement();
        }

    }

    private void Update()
    {
        //If theres an action to be called
        if (_inputAction != null)
        {
            //and the value being read is non-zero
            ReadHorizontalMovement();

        }
    }

    private void FixedUpdate()
    {
        //If theres an action to be called
        if (_inputAction != null)
        {
            //and the value being read is non-zero

            MovePaddle();


        }
    }





    //This is what player gets what
    private void GetHorizontalMovement()
    {
        //Player 1 - Right S, Left A
        //Player 2 - Right K, Left L

        //Test with multiple inputs


        if (playerNumber > 0)
        {
            Debug.Log(playerNumber);
            switch (playerNumber)
            {
                case 1:
                    _inputAction = _playerInputActions.Player.Player1;
                    //Debug.Log(_inputAction.ToString());
                    break;
                case 2:
                    _inputAction = _playerInputActions.Player.Player2;
                    //Debug.Log(_inputAction.ToString());
                    break;
            }


        }


    }

    public void ReadHorizontalMovement()
    {
        _horizontalMovement = _inputAction.ReadValue<float>();
        //var playingOnThis = _playerInput.actions.devices;
        //var playingOnThis = _playerInput.actions.actionMaps;
        //Debug.Log($"Currently playing on: {playingOnThis}");
    }

    void MovePaddle()
    {
        _paddlesVelocity = _paddlesRigidbody.velocity;
        _paddlesVelocity.x = _horizontalMovement * _paddleSpeedModifier * _paddleSpeedModifier;
        _paddlesRigidbody.velocity = _paddlesVelocity;

    }
}

