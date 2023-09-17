using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject CloneWhichBall;

    private GameObject ball;

    void Awake()
    {
        if(CloneWhichBall != null)
        {
            //Spawner is going to be in the middle of the field, so spawn the ball exactly their!
            ball = Instantiate(CloneWhichBall, this.transform, false);
            ball.transform.rotation = Quaternion.identity;
            ball.SetActive(true);
        }
        else
        {
            Debug.Log("Starting Ball Asset Missing!");
        }
    }

}
