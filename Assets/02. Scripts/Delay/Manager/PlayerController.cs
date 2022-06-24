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
    
    
    private bool isTakeHit;
    public bool isCanControll = true;
    


   public int number;
    Vector3 moveVec;

    Animator anim;
    Rigidbody rigid;

    Collider other;

    FieldOfView fieldOfView;
    ICommand attackCommand;

    private static PlayerController instance;
     public static PlayerController Instance
    {
        get
        {
            if(null == instance)
            {
                //게임 인스턴스가 없다면 하나 생성해서 넣어준다.
                instance = new PlayerController();
            }
            return instance;
        }
    }
        
    


    private void Start()
    {
       fieldOfView = GetComponent<FieldOfView>();
    }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        attackCommand = gameObject.AddComponent<AttackCommand>();
        
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
            // isCanControll = false;
            //photonView.RPC("DoSwing", RpcTarget.All);
            attackCommand.Execute();
            TriggerAnim("doHit");
            
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




    public void TriggerAnim(string name)
    {
        anim.SetTrigger(name);
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }


}
