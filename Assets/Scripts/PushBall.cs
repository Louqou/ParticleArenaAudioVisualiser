using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PushBall : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(speed / 2, 0, speed / 3);
    }
}
