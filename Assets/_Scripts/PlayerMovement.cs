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
        _playerInput = GetComponent<PlayerInput>();
        _playerInputActions = new PlayerInputActions();
    }

    private void Start()
    {
        
        GetHorizontalMovement();
    }

    private void Update()
    {
        if (_inputAction != null)
        {
            ReadHorizontalMovement();
        }
    }

    private void FixedUpdate()
    {
        if (_inputAction != null)
        {
            MovePaddle();
        }
    }

    private void OnEnable()
    {
        //Enable all the action maps
        _playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        //Disable all the action maps
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
                    Debug.Log(_inputAction.ToString());
                    break;
                case 2:
                    _inputAction = _playerInputActions.Player.Player2;
                    Debug.Log(_inputAction.ToString());
                    break;  
            }


        }            
      

    }

    public void ReadHorizontalMovement()
    {
        Debug.Log("Before Reading Movement = "+_inputAction.ToString());
        _horizontal = _inputAction.ReadValue<float>() * paddleSpeed;
        Debug.Log("After Reading Movement = " + _horizontal);
    }

    void MovePaddle()
    {
        Vector3 paddleVelocity = paddlesRigidbody.velocity;
        paddleVelocity.x = _horizontal * paddleSpeed;
        paddlesRigidbody.velocity = paddleVelocity;
    }

}
