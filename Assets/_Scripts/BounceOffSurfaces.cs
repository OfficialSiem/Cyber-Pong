using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOffSurfaces : MonoBehaviour
{
    
    public Vector3 puppyCat = Vector3.zero;

    public float howMuchSpeedTooAdd = 0.5f;

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
<<<<<<< Updated upstream
            this.puppyCat = whereTheCollisionIs.normal;
            Debug.DrawRay(whereTheCollisionIs.point, this.puppyCat * 10, Color.black, 10f);

            Debug.Log("Colliding at " + this.puppyCat.ToString());


            
=======
            this.collisionsNormalVector = whereTheCollisionIs.normal;

            //Caculate the reflected path along the collision's normal
            Vector3 reflectionVector = Vector3.Reflect(ball.currentDirection, this.collisionsNormalVector);

            if (ball.currentDirection.x < -maximumComponentThreshold || ball.currentDirection.x > maximumComponentThreshold)
            {

                //if so randomly change X direction
                Vector3 placeHolderVector = ChangeNormalUsingX(ball.currentDirection);
                reflectionVector = Vector3.Reflect(placeHolderVector, this.collisionsNormalVector);
                Debug.DrawRay(whereTheCollisionIs.point, reflectionVector * 10, Color.red, 20f);
                Debug.Log("New Direction Vector Caculated");
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

            //Adding Speed
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream

            //Check if the ball hit something nearly straight on
            if (this.puppyCat.x < -0.94f || this.puppyCat.x > 0.94f)
            {
                Debug.Log("Changing X normal");
                //if so randomly change X direction
                this.puppyCat = ChangeNormalUsingX(this.puppyCat);
                Debug.DrawRay(whereTheCollisionIs.point, this.puppyCat * 10, Color.red, 20f);
            }
            else if (this.puppyCat.z < -0.94f || this.puppyCat.z > 0.94f)
            {
                Debug.Log("Changing Z normal");
                //Or randomly change Z direction
                this.puppyCat = ChangeNormalUsingZ(this.puppyCat);
                Debug.DrawRay(whereTheCollisionIs.point, this.puppyCat * 10, Color.blue, 20f);
            }

            Debug.Log("Attempting To Apply Changes");
            //Then apply those changes
            ball.ChangeBallDirection(this.puppyCat);

=======
            //Finally Change Direction
            ball.ChangeBallDirection(reflectionVector);
>>>>>>> Stashed changes

        }
    }

<<<<<<< Updated upstream
    private static Vector3 ChangeNormalUsingX(Vector3 anotherVector)
=======

    private Vector3 ChangeNormalUsingX(Vector3 anotherVector)
    {
        

        //Is there some semblence of movement (meaning is Z greater than 0)
        // If Z points down,give it a random z value going down
        float zComponent = anotherVector.z < 0.0f ? -Random.Range(minimumComponentThreshold + 0.01f, maximumComponentThreshold - 0.01f) :
            //Else give it a random z value thats pointing upward!
            Random.Range(minimumComponentThreshold + 0.01f, maximumComponentThreshold - 0.01f);

        Debug.Log($"New zComponent: {zComponent}");

        // Calculate coresponding x component using Pythagoras c^2 = a^2 + b^2
        float aSquaredXComponent = 1 - Mathf.Pow(zComponent, 2);

        Debug.Log($"New aSquaredXComponent: {aSquaredXComponent}");

        float xComponent = Mathf.Sqrt(aSquaredXComponent);

        Debug.Log($"New xComponent: {xComponent}");


        anotherVector.x = xComponent;
        anotherVector.z = zComponent;

        return anotherVector;
    }
    
    
    
    
    private Vector3 ChangeNormalUsingZ(Vector3 anotherVector)
>>>>>>> Stashed changes
    {
        //Grab the components
        float xComponent = anotherVector.x;
        float zComponent = anotherVector.z;
        
        //If X is basically hitting near dead center
        if (xComponent > 0.94f)
        {
            // Flip a coin for if the ball is going to go up or down
            // If Z points down,give it a random z value going down
            zComponent = Random.value < 0.5f ? -Random.Range(0.34f, 0.93f) :
                //Else give it a random z value thats pointing upward!
                Random.Range(0.34f, 0.93f);

            // Calculate coresponding x component using Pythagoras c^2 = a^2 + b^2
            float aSquaredXComponent = 1 + Mathf.Pow(zComponent, 2);
            xComponent = Mathf.Sqrt(aSquaredXComponent);

<<<<<<< Updated upstream
            //Then point X in the opposite direction!
            //xComponent = -xComponent;
        }
=======
        // Flip a coin for if the ball is going to go up or down
        // If X points down,give it a random x value going backward
        float xComponent = anotherVector.x < 0.0f ? -Random.Range(minimumComponentThreshold + 0.01f, maximumComponentThreshold - 0.01f) :
            //Else give it a random x value thats pointing foward!
            Random.Range(minimumComponentThreshold + 0.01f, maximumComponentThreshold - 0.01f);
>>>>>>> Stashed changes

        //Do the same as the above for the opposite direction!
        else if (xComponent < -0.94f)
        {
            // Flip a coin for if the ball is going to go up or down
            // If Z points down,give it a random z value going down
            zComponent = Random.value < 0.5f ? -Random.Range(0.34f, 0.93f) :
                //Else give it a random z value thats pointing upward!
                Random.Range(0.34f, 0.93f);

<<<<<<< Updated upstream
            // Calculate coresponding x component using Pythagoras c^2 = a^2 + b^2
            float squaredXComponent = 1 + Mathf.Pow(zComponent, 2);
            xComponent = Mathf.Sqrt(squaredXComponent);

            //Then point X in the opposite direction!
            //xComponent = -xComponent;
        }
=======
>>>>>>> Stashed changes

        anotherVector.x = xComponent;
        anotherVector.z = zComponent;

        return anotherVector;
    }

    private static Vector3 ChangeNormalUsingZ(Vector3 anotherVector)
    {
        //Grab the components
        float xComponent = anotherVector.x;
        float zComponent = anotherVector.z;

        //If Z is basically hitting near dead center
        if (zComponent > 0.94f)
        {
            // Flip a coin for if the ball is going to go up or down
            // If X points left,give it a random x value going left
            xComponent = Random.value < 0.5f ? -Random.Range(0.34f, 0.93f) :
                //Else give it a random x value thats pointing right!
                Random.Range(0.34f, 0.93f);

            // Calculate coresponding z component using Pythagoras c^2 = a^2 + b^2
            float squaredZComponent = 1 + Mathf.Pow(zComponent, 2);
            zComponent = Mathf.Sqrt(squaredZComponent);

            //Then point Z in the opposite direction!
            zComponent = -zComponent;
        }

        //Do the same as the above for the opposite direction!
        else if (zComponent < -0.94f)
        {
            // Flip a coin for if the ball is going to go up or down
            // If X points left,give it a random x value going left
            xComponent = Random.value < 0.5f ? -Random.Range(0.34f, 0.93f) :
                //Else give it a random x value thats pointing right!
                Random.Range(0.34f, 0.93f);

            // Calculate coresponding z component using Pythagoras c^2 = a^2 + b^2
            float squaredZComponent = 1 + Mathf.Pow(zComponent, 2);
            zComponent = Mathf.Sqrt(squaredZComponent);

            //Then point Z in the opposite direction!
            zComponent = -zComponent;
        }

        anotherVector.x = xComponent;
        anotherVector.z = zComponent;
        Debug.Log($"X Changes Made: {anotherVector}");
        return anotherVector;
    }



  
}
