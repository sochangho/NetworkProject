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
    


   public int number;
    Vector3 moveVec;

    Animator anim;
    Rigidbody rigid;

    Collider other;

    FieldOfView fieldOfView;
    


    private void Start()
    {
       fieldOfView = GetComponent<FieldOfView>();
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
        

        Invoke("DoSwingOut", 0.6f);

    }

    public void DoSwingOut()
    {
        isHit = false;
        isCanControll = true;
    }


    public void Attack()
    {
        fieldOfView.FindVisibleTargets();
    }

    
    

    

   public void Hit(Collider collider)
   {
        photonView.RPC("TakeHit", RpcTarget.All,collider.transform.root.forward.x,collider.transform.root.forward.z);
   }

    [PunRPC]
    public void TakeHit(float _x, float _z)
    {
        isTakeHit = true;
        isCanControll = false;

        Vector3 direction = new Vector3(_x,4,_z);
        anim.SetBool("isTakeHit",true);
        rigid.velocity = direction.normalized * 3;

        Destroy(gameObject,0.5f);
    }

    /*
    public void TakeHitOut()
    {
        anim.SetBool("isTakeHit",false);
        isTakeHit = false;
        isCanControll = true;
    }
    */






    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }


}
