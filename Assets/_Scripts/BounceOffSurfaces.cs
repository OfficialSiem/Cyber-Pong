using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOffSurfaces : MonoBehaviour
{
    
    public Vector3 collisionsNormalVector = Vector3.zero;

    public float howMuchSpeedTooAdd = 0.5f;

    [Range(0.191f, 0.982f)]
    public float maximumComponentThreshold = 0.97f;

    public float minimumComponentThreshold = 0.0f;

    public void Awake()
    {
        //Calculate the minimum distance we can use before triggering this sequence
        // Calculate coresponding x component using Pythagoras c^2 - b^2 = a^2
        float minmumComponenntThresholdSquared = 1 - Mathf.Pow(maximumComponentThreshold, 2);
        Debug.Log($"New minmumComponenntThresholdSquared: {minmumComponenntThresholdSquared}");

        minimumComponentThreshold = Mathf.Sqrt(minmumComponenntThresholdSquared);
    }

    public enum CustomForceType
    {
        Additive,
        Mulitiplicative
    }

    public CustomForceType forceType = CustomForceType.Additive;

    private void OnCollisionEnter(Collision collision)
    {
        
        Ball ball = collision.gameObject.GetComponent<Ball>();


        //If you actual collided into the ball
        if (ball != null)
        {
            //Get the normal of the collision vector
            var whereTheCollisionIs = collision.GetContact(0);
            this.collisionsNormalVector = whereTheCollisionIs.normal;
           
            Debug.DrawRay(whereTheCollisionIs.point, this.collisionsNormalVector * 10, Color.black, 10f);

            Debug.Log("Colliding at " + this.collisionsNormalVector.ToString());

            Vector3 holdingNewVector = Vector3.zero;
                   
            switch (forceType)
            {
                case CustomForceType.Additive:
                    ball.currentSpeed += howMuchSpeedTooAdd;
                    break;

                case CustomForceType.Mulitiplicative:
                    ball.currentSpeed *= howMuchSpeedTooAdd;
                    break;
            }

            Debug.DrawRay(whereTheCollisionIs.point, -ball.currentDirection * 10, Color.magenta, 10f);
            Debug.DrawRay(whereTheCollisionIs.point, ball.currentDirection * 10, Color.green, 10f);
            //Check if the ball hit something nearly straight on
            if (ball.currentDirection.x < -maximumComponentThreshold || ball.currentDirection.x > maximumComponentThreshold)
            {
                Debug.Log("Changing X normal");
                //if so randomly change X direction
                holdingNewVector = ChangeNormalUsingX(ball.currentDirection);
                Debug.DrawRay(whereTheCollisionIs.point, holdingNewVector * 10, Color.red, 20f);
                Debug.Log("New Direction Vector Caculated");
                //Then apply those changes
                ball.ChangeBallDirection(holdingNewVector);
            }
            else if (ball.currentDirection.z < -maximumComponentThreshold || ball.currentDirection.z > maximumComponentThreshold)
            {
                Debug.Log("Changing Z normal");
                //Or randomly change Z direction
                holdingNewVector = ChangeNormalUsingZ(ball.currentDirection);
                Debug.DrawRay(whereTheCollisionIs.point, holdingNewVector * 10, Color.blue, 20f);
                Debug.Log("New Direction Vector Caculated");
                //Then apply those changes
                ball.ChangeBallDirection(holdingNewVector);
            }




        }

        //See if you collided into ANYTHING and grab its "ball" component


    }

    
    private Vector3 ChangeNormalUsingX(Vector3 anotherVector)
    {

        Debug.Log($"New minmumComponenntThreshold: {minimumComponentThreshold}");

        // Flip a coin for if the ball is going to go up or down
        // If Z points down,give it a random z value going down
        float zComponent = Random.value < 0.5f ? -Random.Range(minimumComponentThreshold + 0.01f, maximumComponentThreshold - 0.01f) :
            //Else give it a random z value thats pointing upward!
            Random.Range(minimumComponentThreshold + 0.01f, maximumComponentThreshold - 0.01f);

        Debug.Log($"New zComponent: {zComponent}");

        // Calculate coresponding x component using Pythagoras c^2 = a^2 + b^2
        float aSquaredXComponent = 1 - Mathf.Pow(zComponent, 2);

        Debug.Log($"New aSquaredXComponent: {aSquaredXComponent}");

        float xComponent = Mathf.Sqrt(aSquaredXComponent);

        Debug.Log($"New xComponent: {xComponent}");

        //Then point X in the opposite direction!
        xComponent = -xComponent;

        anotherVector.x = xComponent;
        anotherVector.z = zComponent;
        Debug.Log($"X Changes Made: {anotherVector}");
        return anotherVector;
    }
    
    
    
    
    private Vector3 ChangeNormalUsingZ(Vector3 anotherVector)
    {
        //Grab the components
        float zComponent = anotherVector.z;


        // Flip a coin for if the ball is going to go up or down
        // If X points down,give it a random x value going backward
        float xComponent = Random.value < 0.5f ? -Random.Range(minimumComponentThreshold + 0.01f, maximumComponentThreshold - 0.01f) :
            //Else give it a random x value thats pointing foward!
            Random.Range(minimumComponentThreshold + 0.01f, maximumComponentThreshold - 0.01f);

        // Calculate coresponding z component using Pythagoras c^2 = a^2 + b^2
        float aSquaredXComponent = 1 - Mathf.Pow(zComponent, 2);
        zComponent = Mathf.Sqrt(aSquaredXComponent);

        //Then point z in the opposite direction!
        zComponent = -zComponent;

        anotherVector.x = xComponent;
        anotherVector.z = zComponent;
        Debug.Log($"X Changes Made: {anotherVector}");
        return anotherVector;
    }
    


  
}
