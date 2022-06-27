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
    public float detectSize;
    


    public int number;
    Vector3 moveVec;

    Animator anim;
    Rigidbody rigid;

    Collider other;

    FieldOfView fieldOfView;
    ICommand attackCommand;
    ObjChecker objChecker;

    public Transform dotPos;

    public ParticleSystem deadEffect;



    

    
    
    private void Start()
    {
       fieldOfView = GetComponent<FieldOfView>();
       objChecker = new SphereObjChecker(this);
    }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        attackCommand = GetComponent<AttackCommand>();

        
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
            attackCommand.Execute();
            //TriggerAnim("doHit"); 
        }



    }
    

    public void Moving()
    {   

        if (!isCanControll)
            return;

        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        anim.SetBool("isRun", hAxis != 0 || vAxis != 0);

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        transform.LookAt(transform.position + moveVec);

        if(objChecker.IsPerceive())
            return;

        transform.position += moveVec * speed * Time.deltaTime;
        
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

        StopCoroutine("CEffectDelay");
        StartCoroutine("CEffectDelay");
    }
    
    IEnumerator CEffectDelay()
    {
        yield return new WaitForSeconds(0.25f);
        
        Destroy(gameObject);

        Instantiate(deadEffect,transform.position,Quaternion.identity);
    }




    public void TriggerAnim(string name)
    {
        anim.SetTrigger(name);
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }


}
