using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;



namespace Changho
{

    public class PlayerCameraSet : MonoBehaviour
    {



        private PhotonView photonViewPlayer;



        public void Start()
        {
            CameraFollow();
        }


        public void CameraFollow()
        {


            transform.rotation = Quaternion.Euler(new Vector3(60, 0, 0));

            PhotonView[] photonViews = FindObjectsOfType<PhotonView>();

            for (int i = 0; i < photonViews.Length; i++)
            {
                if (photonViews[i].IsMine)
                {
                    photonViewPlayer = photonViews[i];
                    break;
                }

            }

            StartCoroutine(CameraFollowRoutin());

        }




        IEnumerator CameraFollowRoutin()
        {

            while (photonViewPlayer != null)
            {


                transform.position = new Vector3(photonViewPlayer.transform.position.x,
                photonViewPlayer.transform.position.y + 2,
                photonViewPlayer.transform.position.z - 1);

                yield return null;
            }


        }




    }
}