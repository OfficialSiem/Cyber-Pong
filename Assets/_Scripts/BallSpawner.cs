using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject CloneWhichBall;

    [SerializeField]
    private Vector3 locationInWorldSpace = new Vector3(0.0f,7.2f,0.0f);


    private GameObject ball;

    void Awake()
    {
        if(CloneWhichBall != null)
        {
            //Spawner is going to be in the middle of the field, so spawn the ball exactly their!

            ball = Instantiate(CloneWhichBall, locationInWorldSpace, Quaternion.identity);
            ball.SetActive(true);
        }
        else
        {
            Debug.Log("Starting Ball Asset Missing!");
        }
    }

}
