using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Jiyeon;
public class PlayerController : MonoBehaviourPun, IPunObservable
{
    private float hAxis;
    private float vAxis;

    public float speed;
    private bool isHit;

    public int killCount;

    private bool isCanControll = true;

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
        //if (!photonView.IsMine)
        //    return;

        Moving();
        //DoSwing();

    }

    private void Update()
    {
        if (!photonView.IsMine)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
            photonView.RPC("DoSwing", RpcTarget.All);


    }

    public void Moving()
    {
        if (isCanControll)
        {
            hAxis = Input.GetAxisRaw("Horizontal");
            vAxis = Input.GetAxisRaw("Vertical");

            moveVec = new Vector3(hAxis, 0, vAxis).normalized;
            transform.position += moveVec * speed * Time.deltaTime;

            anim.SetBool("isRun", moveVec != Vector3.zero);

            transform.LookAt(transform.position + moveVec);


        }

    }

    //[PunRPC]
    public void DoSwing()
    {
        isCanControll = false;

        anim.SetTrigger("doHit");
        WeaponBat.batUse(); // WeaponBat.Use()

        Invoke("DoSwingOut", 0.6f);




    }

    public void DoSwingOut()
    {
        isHit = false;
        isCanControll = true;
    }






    private void OnCollisionEnter(Collision collision)
    {
        //  Collider[] a= Physics.BoxCastAll();
        if (collision.gameObject.tag == "melee")
        {
            photonView.RPC("TakeHit", RpcTarget.All);
        }

    }


    //[PunRPC]
    public void TakeHit()
    {

        isCanControll = false;
        anim.SetBool("isTakeHit", true);


    }




    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }


}
