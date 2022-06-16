using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Changho.Managers;
using Changho.Interface;
namespace Changho.UI
{
    public class CrearteRoomPopup : Popup , Iinfomation
    {
        public int maxPlayerCnt = 5;

        [SerializeField]
        private InputField inputFieldName;
        [SerializeField]
        private InputField inputFieldPlayerCnt;

        [SerializeField]
        private Button buttonCreateRoom;
        [SerializeField]
        private Button buttonCancel;


        [SerializeField]
        private InfoText infoText;



        private InfoText cloneInfoText;
        private Lobby.Lobby lobby;

        public void Awake()
        {
            lobby = FindObjectOfType<Lobby.Lobby>();
        }

        public void Start()
        {
           
            OnOpen();

            buttonCreateRoom.onClick.AddListener(OnCreateRoomClick);
            buttonCancel.onClick.AddListener(OnClose);
            
        }

        private void OnCreateRoomClick()
        {
            int playerCnt = int.Parse(inputFieldPlayerCnt.text);

            if (inputFieldName.text == "")
            {

                Infomation("re-enter");
                return;

            }


            if(playerCnt == 0 || playerCnt > maxPlayerCnt)
            {
                Infomation("Please re-enter the player number....");
                return; 

            }


            LobbyManager.Instance.CreateRoom(inputFieldName.text, (byte)playerCnt);
             
        } 



        //override--------------
        public override void OnOpen()
        {
            base.OnOpen();
            lobby.ButtonsInteractable(false);
        }


        public override void OnClose()
        {
            lobby.ButtonsInteractable(true);
            base.OnClose();
            
        }


        public void Infomation(string str)
        {
            if (cloneInfoText != null)
            {
                Destroy(cloneInfoText.gameObject);
            }

            cloneInfoText = Instantiate(infoText);
            cloneInfoText.transform.parent = this.transform;
            cloneInfoText.GetComponent<RectTransform>().localPosition = new Vector2(0.5f, 126.4f);
            cloneInfoText.InfoTextSet(str);


        }

        public void ServerInfomation(string str)
        {

        }


    }
}