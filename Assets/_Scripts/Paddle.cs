using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float _paddleBaseSpeed = 12.5f;

    public float GetPaddleBaseSpeed()
    {
        return _paddleBaseSpeed;
    }

    public Vector3 GetPaddleVelocity()
    {
        return GetComponent<Rigidbody>().velocity;
    }

}
