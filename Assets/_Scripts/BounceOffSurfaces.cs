using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOffSurfaces : MonoBehaviour
{
    
    public Vector3 collisionsNormalVector = Vector3.zero;

    public float howMuchSpeedTooAdd = 0.5f;

    [Range(0.191f, 0.982f)]
    public float maximumComponentThreshold = 0.9f;

    public float minimumComponentThreshold = 0.6f;

    public void Awake()
    {
        //Calculate the minimum distance we can use before triggering this sequence
        // Calculate coresponding x component using Pythagoras c^2 - b^2 = a^2
        //float minmumComponenntThresholdSquared = 1 - Mathf.Pow(maximumComponentThreshold, 2);
        //Debug.Log($"New minmumComponenntThresholdSquared: {minmumComponenntThresholdSquared}");

        //minimumComponentThreshold = Mathf.Sqrt(minmumComponenntThresholdSquared);
    }

    public enum CustomForceType
    {
        Additive,
        Mulitiplicative
    }

    public CustomForceType forceType = CustomForceType.Additive;

    private void OnCollisionEnter(Collision collision)
    {

        //See if you collided with the ball
        BallMovement ball = collision.gameObject.GetComponent<BallMovement>();

        //If you actual collided into the ball
        if (ball != null)
        {
            //Get the normal of the collision vector
            var whereTheCollisionIs = collision.GetContact(0);
            this.collisionsNormalVector = whereTheCollisionIs.normal;

            //Caculate the reflected path along the collision's normal
            Vector3 reflectionVector = Vector3.Reflect(ball.currentDirection, this.collisionsNormalVector);
            Debug.DrawRay(whereTheCollisionIs.point, reflectionVector * 10, Color.red, 20f);
            Debug.Log("I collided, and drew a ray");
            //If it gets to close to a ceratain threshold number
            reflectionVector = CheckIfBallHitsToStraightOnToAnything(ball, whereTheCollisionIs, reflectionVector);


            /*if (ball.currentDirection.x < -maximumComponentThreshold || ball.currentDirection.x > maximumComponentThreshold)
            {

                //if so randomly change X direction
                Vector3 placeHolderVector = ChangeNormalUsingX(ball.currentDirection);
                reflectionVector = Vector3.Reflect(placeHolderVector, this.collisionsNormalVector);
                Debug.DrawRay(whereTheCollisionIs.point, reflectionVector * 10, Color.red, 20f);
                //Debug.Log("New Direction Vector Caculated");
                //Then apply those changes

            }
            else if (ball.currentDirection.z < -maximumComponentThreshold || ball.currentDirection.z > maximumComponentThreshold)
            {
                Debug.Log("Changing Z normal");
                //Or randomly change Z direction
                Vector3 holdingNewVector = ChangeNormalUsingZ(ball.currentDirection);
                reflectionVector = Vector3.Reflect(holdingNewVector, this.collisionsNormalVector);
                Debug.DrawRay(whereTheCollisionIs.point, reflectionVector * 10, Color.blue, 20f);



            }
*/
            //Adding Speed
            switch (forceType)
            {
                case CustomForceType.Additive:
                    ball.currentSpeed += howMuchSpeedTooAdd;
                    //Check for PlayerMovement, then add a bit of the Paddle Velocity to Balls
                    break;

                case CustomForceType.Mulitiplicative:
                    ball.currentSpeed *= howMuchSpeedTooAdd;
                    break;
            }

            //Finally Change Direction
            ball.ChangeBallDirection(reflectionVector);

        }
    }

    private Vector3 CheckIfBallHitsToStraightOnToAnything(BallMovement ball, ContactPoint whereTheCollisionIs, Vector3 reflectionVector)
    {
        if (Vector3.Dot(Vector3.right, reflectionVector) > maximumComponentThreshold)
        {
            Debug.Log("A Unique Threshold Was Reached Right, and I drew a ray " + Vector3.Dot(Vector3.right, reflectionVector));
            //if so randomly change X direction
            Vector3 placeHolderVector = ChangeNormalUsingX(ball.currentDirection);
            reflectionVector = Vector3.Reflect(placeHolderVector, this.collisionsNormalVector);
            Debug.DrawRay(whereTheCollisionIs.point, reflectionVector * 10, Color.yellow, 30f);
            //Debug.Log("New Direction Vector Caculated");
            //Then apply those changes
        }
        else if (Vector3.Dot(Vector3.left, reflectionVector) > maximumComponentThreshold)
        {
            Debug.Log("A Unique Threshold Was Reached Left, and I drew a ray " + Vector3.Dot(Vector3.left, reflectionVector));
            //if so randomly change X direction
            Vector3 placeHolderVector = ChangeNormalUsingX(ball.currentDirection);
            reflectionVector = Vector3.Reflect(placeHolderVector, this.collisionsNormalVector);
            Debug.DrawRay(whereTheCollisionIs.point, reflectionVector * 10, Color.cyan, 30f);
            //Debug.Log("New Direction Vector Caculated");
            //Then apply those changes
        }
        else if (Vector3.Dot(Vector3.up, reflectionVector) > maximumComponentThreshold)
        {

            Debug.Log("A Unique Threshold Was Reached Up, and I drew a ray " + Vector3.Dot(Vector3.up, reflectionVector));
            //Or randomly change Z direction
            Vector3 holdingNewVector = ChangeNormalUsingZ(ball.currentDirection);
            reflectionVector = Vector3.Reflect(holdingNewVector, this.collisionsNormalVector);
            Debug.DrawRay(whereTheCollisionIs.point, reflectionVector * 10, Color.magenta, 30f);
        }
        else if (Vector3.Dot(Vector3.down, reflectionVector) > maximumComponentThreshold)
        {
            Debug.Log("A Unique Threshold Was Reached Down, and I drew a ray " + Vector3.Dot(Vector3.down, reflectionVector));
            //Or randomly change Z direction
            Vector3 holdingNewVector = ChangeNormalUsingZ(ball.currentDirection);
            reflectionVector = Vector3.Reflect(holdingNewVector, this.collisionsNormalVector);
            Debug.DrawRay(whereTheCollisionIs.point, reflectionVector * 10, Color.black, 30f);
        }

        return reflectionVector;
    }

    private Vector3 ChangeNormalUsingX(Vector3 anotherVector)
    {
        

        //Is there some semblence of movement (meaning is Z greater than 0)
        // If Z points down,give it a random z value going down
        float zComponent = anotherVector.z < 0.0f ? -Random.Range(minimumComponentThreshold + 0.01f, maximumComponentThreshold - 0.01f) :
            //Else give it a random z value thats pointing upward!
          Random.Range(minimumComponentThreshold + 0.01f, maximumComponentThreshold - 0.01f);

        //Debug.Log($"New zComponent: {zComponent}");

        // Calculate coresponding x component using Pythagoras c^2 = a^2 + b^2
        float aSquaredXComponent = 1 - Mathf.Pow(zComponent, 2);

        //Debug.Log($"New aSquaredXComponent: {aSquaredXComponent}");

        float xComponent = Mathf.Sqrt(aSquaredXComponent);

        //Debug.Log($"New xComponent: {xComponent}");


        anotherVector.x = xComponent;
        anotherVector.z = zComponent;

        return anotherVector;
    }
    
    
    
    
    private Vector3 ChangeNormalUsingZ(Vector3 anotherVector)
    {
        //Grab the components
        float zComponent = anotherVector.z;


        // Flip a coin for if the ball is going to go up or down
        // If X points down,give it a random x value going backward
        float xComponent = anotherVector.x < 0.0f ? -Random.Range(minimumComponentThreshold + 0.01f, maximumComponentThreshold - 0.01f) :
            //Else give it a random x value thats pointing foward!
            Random.Range(minimumComponentThreshold + 0.01f, maximumComponentThreshold - 0.01f);

        // Calculate coresponding z component using Pythagoras c^2 = a^2 + b^2
        float aSquaredXComponent = 1 - Mathf.Pow(zComponent, 2);
        zComponent = Mathf.Sqrt(aSquaredXComponent);


        anotherVector.x = xComponent;
        anotherVector.z = zComponent;

        return anotherVector;
    }
    


  
}
