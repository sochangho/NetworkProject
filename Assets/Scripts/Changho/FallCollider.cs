using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

using Changho.Managers;

namespace Changho
{
    public class FallCollider : MonoBehaviourPun
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PhotonView>() != null )
            {

                int playerActor = other.GetComponent<PlayerController>().number;

               
                photonView.RPC("FallPlayer", RpcTarget.All, playerActor);
            }
        }

        [PunRPC]
        public void FallPlayer()
        {

           
            Debug.Log("FallPlayer");
            gameObject.SetActive(false);
            gameObject.GetComponent<PlayerController>().isCanControll = false;
            NetGameManager.Instance.Respwan(gameObject);

        }



    }
}