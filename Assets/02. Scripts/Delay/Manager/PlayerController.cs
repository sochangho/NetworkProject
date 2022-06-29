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
    private float defaultSpeed;
    public TrailRenderer trailEffect;


    private bool isTakeHit;
    private bool isDash;
    
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
    //public ParticleSystem buffEffect;




   


    private void Start()
    {
        isDash = true;
        defaultSpeed = speed;
        fieldOfView = GetComponent<FieldOfView>();
        objChecker = new SphereObjChecker(this);
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
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
        }

        if (Input.GetKey(KeyCode.LeftShift) && isDash == true)
        {
            trailEffect.enabled = true;
            StartCoroutine(CDashTime());
            
        }
        else
        {
            
            defaultSpeed = speed;
        }



    }

    IEnumerator CDashTime()
    {

        defaultSpeed = 3;
        yield return new WaitForSeconds(0.8f);
        trailEffect.enabled = false;
        StartCoroutine(CDashCoolTime());
    }
    IEnumerator CDashCoolTime()
    {
        isDash = false;
        
        yield return new WaitForSeconds(5.0f);
        isDash = true;
        
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

        if (objChecker.IsPerceive())
            return;

        transform.position += moveVec * defaultSpeed * Time.deltaTime;

    }

    public void Dash()
    {
        if (!isCanControll)
            return;

        defaultSpeed = 1;

        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        anim.SetBool("isRun", hAxis != 0 || vAxis != 0);

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        transform.LookAt(transform.position + moveVec);

        if (objChecker.IsPerceive())
            return;

        transform.position += moveVec * defaultSpeed;
    }



    public void Attack()
    {
        fieldOfView.FindVisibleTargets();

        if (!photonView.IsMine) 
        { return; }

     
    }

    public void Hit(Collider collider)
    {
        photonView.RPC("TakeHit", RpcTarget.All, collider.transform.root.forward.x, collider.transform.root.forward.z);
        
    }

    [PunRPC]
    public void TakeHit(float _x, float _z)
    {
        isTakeHit = true;
        isCanControll = false;

        Vector3 direction = new Vector3(_x, 4, _z);
        anim.SetBool("isTakeHit", true);
        rigid.velocity = direction.normalized * 3;

        StopCoroutine("CEffectDelay");
        StartCoroutine("CEffectDelay");
    }

    IEnumerator CEffectDelay()
    {
        yield return new WaitForSeconds(0.25f);

       
        
        
        Instantiate(deadEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Changho.Managers.NetGameManager.Instance.Respwan(number);


    }




    public void TriggerAnim(string name)
    {
        anim.SetTrigger(name);
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }


     void OnTriggerEnter(Collider other)
    {
     if(other.tag == "Fall")
        {

            photonView.RPC("FallPlayer", RpcTarget.All);
        }
        
    }



    [PunRPC]
    public void FallPlayer()
    {


        Debug.Log("FallPlayer");
        Destroy(gameObject);
        Changho.Managers.NetGameManager.Instance.Respwan(number);

    }



}
