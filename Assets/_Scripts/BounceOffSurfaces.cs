using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOffSurfaces : MonoBehaviour
{
    [SerializeField]
    public float howMuchSpeedTooAdd = 2.0f;

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
            Vector3 normal = collision.GetContact(0).normal;

            switch (forceType)
            {
                case CustomForceType.Additive:
                    ball.currentSpeed += howMuchSpeedTooAdd;
                    return;

                case CustomForceType.Mulitiplicative:
                    ball.currentSpeed *= howMuchSpeedTooAdd;
                    return;
            }



            //Use negative to push ball away!
            //ball.AddOutsideForce(-normal * howMuchSpeedTooAdd);

        }


        //See if you collided into ANYTHING and grab its "ball" component
        

       

    }
}
