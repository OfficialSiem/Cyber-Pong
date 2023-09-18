using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Paddle : MonoBehaviour
{

    public float paddleSpeed = 5.0f;

    protected Rigidbody paddlesRigidbody;

    private void Awake()
    {
        paddlesRigidbody = GetComponent<Rigidbody>();
    }

}
