using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMove : MonoBehaviour
{
    Rigidbody rb;
    float speed = 1f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        Vector3 getVel = new Vector3(xMove, 0, zMove) * speed;
        rb.velocity = getVel;
        rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(getVel), 0.15f);
    }
}