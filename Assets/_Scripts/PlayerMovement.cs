using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Paddle
{
    private Vector3 direction;

    private void Update()
    {
        //Player 1 - Forward S, Backward A
        //Player 2 - Forward K, Backwars L

        //Paddle is oriented 90 degres, so move along X instead of Y or Z
         if(Input.GetKey(KeyCode.A))
        {
            direction = Vector3.forward * -1;
        }
         else if(Input.GetKey(KeyCode.S))
        {
            direction = Vector3.forward;
        }
         else
        {
            direction = Vector3.zero;
        }

    }

    private void FixedUpdate()
    {
        // Calculating the squared magnitude instead of using the magnitude property is much faster -
        // the calculation is basically the same only without the slow Sqrt call.

        //If the sqrMagnitude is non-zero and non-negative
        //(sqrMagnitude can never be but good to have that written logically)
        if (direction.sqrMagnitude > 0)
        {
            paddlesRigidbody.AddForce(direction * this.paddleSpeed);
        }

    }
}
