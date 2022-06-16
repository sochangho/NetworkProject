using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Changho.Interface;


namespace Changho.Managers
{
    public class LoginSceneManager : GamePhotonManager<LoginSceneManager>
    {
        Iinfomation iinfomation;
        

        #region UnityEvents

        public override void Awake()
        {
            base.Awake();

           iinfomation = FindObjectOfType<Changho.Login.Login>();
        }

        #endregion


        #region PhotonCallBacks

        public override void OnConnectedToMaster()
        {
            Debug.Log(PhotonNetwork.LocalPlayer.NickName + " : Master Connect");
            iinfomation.ServerInfomation(PhotonNetwork.LocalPlayer.NickName + " : Master Connect");
            TransitionScene("LobbyScene");
            
        }

  

        #endregion


        public void Connect(string playerName)
        {

            PhotonNetwork.LocalPlayer.NickName = playerName;

            PhotonNetwork.ConnectUsingSettings();


        }

    }
}
