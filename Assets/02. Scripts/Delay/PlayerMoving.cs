using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

namespace Jiyeon
{
    public class PlayerMoving : MonoBehaviourPun, IPunObservable
    {
        public float speed = 20f;

        bool isHit;
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
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            movement = new Vector3(x, 0.0f, z).normalized * speed * Time.deltaTime;
        }

        private void FixedUpdate()
        {
            Move();
            DoSwing();
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

        public void DoSwing()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
            {
                anim.SetTrigger("doHit");
                isHit = true;

                Invoke("DoSwingOut", 0.4f);

                //TODO: 스윙 애니메이션, 무적시간추가,
            }
        }

        public void DoSwingOut()
        {
            isHit = false;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {

        }


    }

}