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
    private bool isTakeHit;
    private bool isCanControll = true;

    Vector3 moveVec;

    Animator anim;
    Rigidbody rigid;

    public WeaponBat bat;


    private void Start()
    {

    }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
            return;

        Moving();

    }

    private void Update()
    {



        if (!photonView.IsMine)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            photonView.RPC("DoSwing", RpcTarget.All);
        }



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

    [PunRPC]
    public void DoSwing()
    {
        isHit = true;
        isCanControll = false;

        anim.SetTrigger("doHit");
        bat.Use();

        Invoke("DoSwingOut", 0.6f);

    }

    public void DoSwingOut()
    {
        isHit = false;
        isCanControll = true;
    }



    private void OnTriggerEnter(Collider collider)
    {
        if (!photonView.IsMine)
            return;

        if (collider.gameObject.tag == "Melee")
        {
            photonView.RPC("TakeHit", RpcTarget.All);
        }

    }


    [PunRPC]
    public void TakeHit()
    {
        isTakeHit = true;
        isCanControll = false;


        anim.SetBool("isTakeHit", true);
        rigid.AddForce(Vector3.up * 3, ForceMode.Impulse);
        Invoke("TakeHitOut", 3.0f);
    }
    public void TakeHitOut()
    {
        isTakeHit = false;
        isCanControll = true;
    }






    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }


}
