using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : Paddle
{
    [Tooltip("Is this Player 1, 2, 3, 4.. etc?")]
    [SerializeField] public int playerNumber = 1;

    float _horizontal = 0.0f;

    #region Private Player Attributes
    //Controller for Left/Right Player movement (Joystick, button clicks, etc [determined in Player settings])
    private string _horizontalAxis = null;
    #endregion 

    private void Start()
    {
        GetHorzontalInput();
    }


    private void Update()
    {
        ReadHorizontalInput();
        MovePaddle();

    }

    private void GetHorzontalInput()
    {
        //Player 1 - Forward S, Backward A
        //Player 2 - Forward K, Backwars L

        //Test with multiple inputs


        if (playerNumber > 0 )
        {
            if(_horizontalAxis == null)
            {

                Debug.Log($"Horizontal{playerNumber}");
                _horizontalAxis = $"Horizontal{playerNumber}";
            }

        }            
      

    }
    void ReadHorizontalInput()
    {
        
        _horizontal = Input.GetAxis(_horizontalAxis) * paddleSpeed;
    }

    void MovePaddle()
    {
        Vector3 paddleVelocity = paddlesRigidbody.velocity;
        paddleVelocity.z = _horizontal * paddleSpeed;
        paddlesRigidbody.velocity = paddleVelocity;
    }

}
