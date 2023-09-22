using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : Paddle
{
    [Tooltip("Is this Player 1, 2, 3, 4.. etc?")]
    [SerializeField] public int playerNumber = 1;

    float _horizontal = 0.0f;

    #region Private Player Attributes
    //Controller for Left/Right Player movement (Joystick, button clicks, etc [determined in Player settings])
    private InputAction _inputAction = null;
    private PlayerInput _playerInput = null;
    private PlayerInputActions _playerInputActions = null;
    #endregion 

    private new void Awake()
    {
        base.Awake();
        //_playerInput = GetComponent<PlayerInput>();
        _playerInputActions = new PlayerInputActions();
    }

    private void Start()
    {
        GetHorizontalMovement();
    }

    private void Update()
    {
        //If theres an action to be called
        if (_inputAction != null)
        {
            //and the value being read is non-zero
            if(_inputAction.ReadValue<float>() != 0.0f)
            {
                ReadHorizontalMovement();
            }
            
        }
    }

    private void FixedUpdate()
    {
        //If theres an action to be called
        if (_inputAction != null)
        {
            //and the value being read is non-zero
            if (_inputAction.ReadValue<float>() != 0.0f)
            {
                MovePaddle();
            }
        }
    }

    private void OnEnable()
    {
        //Enable Player Action map
        _playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        //Disable Player Action map
        _playerInputActions.Player.Disable();
    }

    //This is what player gets what
    private void GetHorizontalMovement()
    {
        //Player 1 - Right S, Left A
        //Player 2 - Right K, Left L

        //Test with multiple inputs


        if (playerNumber > 0 )
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
        _horizontal = _inputAction.ReadValue<float>() * paddleSpeed;
    }

    void MovePaddle()
    {
        Vector3 paddleVelocity = paddlesRigidbody.velocity;
        paddleVelocity.x = _horizontal * paddleSpeed;
        paddlesRigidbody.velocity = paddleVelocity;
    }

}
