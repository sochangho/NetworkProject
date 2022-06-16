using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{

    public float speed;
    private float x;
    private float z;

    bool isWalk;

    Vector3 moveVec;
    Animator anim;


    private void Start()
    {

    }
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("vertical");

        moveVec = new Vector3(x, 0, z).normalized;

        transform.position += moveVec * speed * Time.deltaTime;

        anim.SetBool("isWalk", moveVec != Vector3.zero);
    }

}
