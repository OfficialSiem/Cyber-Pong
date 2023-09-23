using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallMovement : MonoBehaviour
{
    [SerializeField]
    private float initalSpeed = 25.0f;

    [SerializeField]
    private float absoluteMaxSpeed = Mathf.Infinity;

    [SerializeField]
    private float velocityX = 0.0f;

    [SerializeField]
    private float velocityZ = 0.0f;

    [SerializeField]
    [Range(0.2f, 0.99f)]
    private float whatPercentageOfSpeedToTakeFromPaddle = 0.25f;

    private Rigidbody ballRigidBody;

    public float currentSpeed = 0f;

    public Vector3 currentDirection = Vector3.zero;

    private void Awake()
    {
        ballRigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //GiveBallRandomStartingSpeed();
        GiveBallASetStartingSpeed();
    }

    private void GiveBallRandomStartingSpeed()
    {
        //Random value chooses a float between 0 and 1, so we can use this as a coin!

        //Therefore flip a coin, change the direction of the ball from going left or right
        this.velocityX = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);

        //Then, flip another coin, change the direction if the ball is going up or down, we'll even give it a random velocity going up and down;
        this.velocityZ = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);

        //Register this as the direction the ball should travel
        Vector3 direction = new Vector3(velocityX, 0 , velocityZ);

        //Add an explosive force for the ball to travel in this direction
        ballRigidBody.AddForce(direction * this.initalSpeed, ForceMode.Impulse);

        string s = direction.ToString();

        //balls current speed is going to be registered

        currentSpeed = initalSpeed;

    }

    private void GiveBallASetStartingSpeed()
    {
       
        //Register this as the direction the ball should travel
        Vector3 direction = new Vector3(velocityX, 0, velocityZ);

        //Add an explosive force for the ball to travel in this direction
        ballRigidBody.AddForce(direction * this.initalSpeed, ForceMode.Impulse);

        string s = direction.ToString();

        //balls current speed is going to be registered

        currentSpeed = initalSpeed;

    }

    //When the ball gets deflected by the paddle
    private void OnCollisionEnter(Collision collision)
    {
        //See if what was collided with was the player
        Paddle _aPaddle = collision.gameObject.GetComponent<Paddle>();
        if (_aPaddle != null) {

            Vector3 _paddleVelocity = _aPaddle.GetPaddleVelocity();
            //If the player was moving
            if (_paddleVelocity.x != 0)
            {
                //add the players speed (which is only in the Z direction) to the ball!
                velocityX += _paddleVelocity.x * whatPercentageOfSpeedToTakeFromPaddle;
            }
            
        }
    }

    private void FixedUpdate()
    {
        //follow the balls direction
        currentDirection = ballRigidBody.velocity.normalized;

        //Get the current speed of the ball, if ball is at MaxSpeed then good- we'll use the speed
        currentSpeed = Mathf.Min(currentSpeed, absoluteMaxSpeed);

        //Caculate the speed of the ball!
        ballRigidBody.velocity = currentDirection * currentSpeed;

    }

    public void ChangeBallDirection(Vector3 anotherDirection)
    {
        ballRigidBody.velocity = anotherDirection * currentSpeed;
    }

}
