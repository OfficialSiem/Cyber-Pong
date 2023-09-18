using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : Paddle
{
    private Vector3 direction;

    private void Update()
    {
        GettingPlayerInput();
        MovePaddle();

    }

    private void GettingPlayerInput()
    {
        //Player 1 - Forward S, Backward A
        //Player 2 - Forward K, Backwars L

        //Test with multiple inputs
        /*
        float movement;
        switch (PlayerID)
        {
            case 1:
                movement = Input.GetAxis("MovePlayer1");
                break;
            case 2:
                movement = Input.GetAxis("MovePlayer2");
                break;
            default:
                break;
        }*/

        //Paddle is oriented 90 degres, so move along X instead of Y or Z
        if (Input.GetKey(KeyCode.A))
        {
            direction = Vector3.forward * -1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction = Vector3.forward;
        }
        else
        {
            direction = Vector3.zero;
        }
    }

    private void MovePaddle()
    {
        //Cant directly set Rigidbody velocity componenets (safty/read write protections)
        //so manually saving the vector, editing, and then reasigning
        Vector3 paddleVelocity = paddlesRigidbody.velocity;
        paddleVelocity.z = direction.z * paddleSpeed;
        paddlesRigidbody.velocity = paddleVelocity;
    }

    /* Using FixedUpdate
    private void FixedUpdate()
    {
        // Calculating the squared magnitude instead of using the magnitude property is much faster -
        // the calculation is basically the same only without the slow Sqrt call.

        //If the sqrMagnitude is non-zero and non-negative
        //(sqrMagnitude can never be but good to have that written logically)
        if (direction.sqrMagnitude > 0)
        {
            paddlesRigidbody.AddForce(direction * this.paddleSpeed, ForceMode.Impulse);
        }

    }
    */
}
