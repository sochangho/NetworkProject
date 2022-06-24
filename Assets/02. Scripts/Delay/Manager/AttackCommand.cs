using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AttackCommand : MonoBehaviourPun, ICommand
{
    private bool isHit;
    PlayerController playerController;
    Animator anim;
    public void Execute()
    {
        PlayerAttack();
    }

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    private void PlayerAttack()
    {
        
        photonView.RPC("DoSwing", RpcTarget.All);
    }

    [PunRPC]
    public void DoSwing()
    {
        isHit = true;
        playerController.isCanControll = false;
        
        anim.SetTrigger("doHit");

        //TODO: 애니메이션 종료 확인 함수 추가

    }

    public void DoSwingOut()
    {
        isHit = false;
        playerController.isCanControll = true;
        
    }
}
