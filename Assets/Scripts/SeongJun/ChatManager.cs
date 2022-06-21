using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public List<string> chatList = new List<string> ();
    public Button chatButton;
    public Text chatLog;
    public Text chatingList;
    public InputField inputField;
    public ScrollRect scroll_rect = null;
    string chatters;
    void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
        scroll_rect = GameObject.FindObjectOfType<ScrollRect> ();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            ChatButtonOnClicked();
        }
    }
    public void ChatButtonOnClicked() {
    //   if (inputField.text.Equals("")) 
    //   { 
    //       return;
    //   }

        string text = string.Format("{0} : {1}",PhotonNetwork.LocalPlayer.NickName,inputField.text);
        photonView.RPC("Receive",RpcTarget.All,text);
        inputField.ActivateInputField();
        inputField.text = "";
    }
    [PunRPC]
    public void Receive(string text) { 
        chatLog.text +="\n" + text;
        scroll_rect.verticalNormalizedPosition = 0;
    }

}
