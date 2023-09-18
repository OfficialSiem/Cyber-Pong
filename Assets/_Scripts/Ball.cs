using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField]
    private float initalSpeed = 25.0f;

    [SerializeField]
    private float absoluteMaxSpeed = Mathf.Infinity;


    private Rigidbody ballRigidBody;

    public float currentSpeed = 0f;

    public Vector3 currentDirection = Vector3.zero;

    private void Awake()
    {
        ballRigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        GiveBallStartingSpeed();
    }

    private void GiveBallStartingSpeed()
    {
        //Random value chooses a float between 0 and 1, so we can use this as a coin!

        //Therefore flip a coin, change the direction of the ball from going left or right
        float velocityX = Random.value < 0.5f ? -1.0f : 1.0f;

        //Then, flip another coin, change the direction if the ball is going up or down, we'll even give it a random velocity going up and down;
        float velocityZ = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);

        //Register this as the direction the ball should travel
        Vector3 direction = new Vector3(velocityX, 0 , velocityZ);

        //Add an explosive force for the ball to travel in this direction
        ballRigidBody.AddForce(direction * this.initalSpeed, ForceMode.Impulse);

        string s = direction.ToString();

        //balls current speed is going to be registered

        currentSpeed = initalSpeed;

    }

    private void FixedUpdate()
    {
        //follow the balls direction
        currentDirection = ballRigidBody.velocity.normalized;

        //Get the current speed of the ball, if ball is at MaxSpeed then good- we'll use the speed
        currentSpeed = Mathf.Min(currentSpeed, absoluteMaxSpeed);

        ballRigidBody.velocity = currentDirection * currentSpeed;

    }

    public void ChangeBallDirection(Vector3 anotherDirection)
    {
        currentDirection = anotherDirection;
        ballRigidBody.velocity = currentDirection * currentSpeed;
        Debug.Log("Ball Changed Successfully!");
    }

}
