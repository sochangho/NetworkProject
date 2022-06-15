using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public float speed = 20f;
    private Vector3 movement;

    private Animator anim;
    private Rigidbody modelRigidBody;

    private void Start()
    {
        anim = GetComponent<Animator>();
        modelRigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        movement = new Vector3(x, 0.0f, z).normalized * speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (movement.magnitude < 0.01f)
        {
            anim.SetBool("isRun", false);
            return;
        }

        modelRigidBody.velocity = movement;
        modelRigidBody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
        anim.SetBool("isRun", true);
    }


}

