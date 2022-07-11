using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
namespace SeongJun
{
    public class ChatManager : MonoBehaviourPunCallbacks
    {
        public Button chatButton;
        public Text chatLog;
        public InputField inputField;
        private ScrollRect scrollRect = null;
        void Start()
        {
            PhotonNetwork.IsMessageQueueRunning = true;
            scrollRect = GameObject.FindObjectOfType<ScrollRect>();
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ChatButtonOnClicked();
            }
        }
        public void ChatButtonOnClicked()
        {
            string text = string.Format("   {0} : {1}", PhotonNetwork.LocalPlayer.NickName, inputField.text);
            photonView.RPC("Receive", RpcTarget.All, text);
            inputField.ActivateInputField();
            inputField.text = "";
        }
        [PunRPC]
        public void Receive(string text)
        {
            chatLog.text += "\n" + text;
            scrollRect.verticalNormalizedPosition = 0;
        }

    }
}