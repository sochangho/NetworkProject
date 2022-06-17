using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using Changho.UI;
using Changho.Managers;

namespace Changho.Room
{
    public class Room : MonoBehaviour
    {
        private RoomManager roomManager;


        //Buttons---------------------------------------
        [SerializeField]
        private Button buttonGameStart;

        [SerializeField]
        private Button buttonReady;

        [SerializeField]
        private Button buttonExit;
        //--------------------------------------------------


        [SerializeField]
        private Transform content;


        [SerializeField]
        private PlayerEntry playerEntry;
        private Dictionary<int , PlayerEntry> playerEntryDic = new Dictionary<int, PlayerEntry>();


        private void Awake()
        {
            roomManager = RoomManager.Instance;

            roomManager.room = this;

            foreach(Player player in PhotonNetwork.PlayerList)
            {
               PlayerEntry peGo = Instantiate(playerEntry);
               peGo.transform.parent = content;
               peGo.GetComponent<RectTransform>().localScale = Vector2.one;

               
                
                if(player.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    peGo.PlayerEntrySet(player.ActorNumber, player.NickName, player);

                }
                else
                {
                    peGo.LoadPlayerEntry(player.ActorNumber, player.NickName, player);
                }

               playerEntryDic.Add(player.ActorNumber, peGo);
            }

            buttonGameStart.onClick.AddListener(OnClickGameStart);
            buttonReady.onClick.AddListener(OnClickReadyButton);
            buttonExit.onClick.AddListener(OnCilckExitRoom);
           
        }

        public void ChangedMaster(Player newMasterClient)
        {

           int num = newMasterClient.ActorNumber;

            if (!playerEntryDic.ContainsKey(num))
            {
                Debug.LogError("ChangedMaster Error");
                return;
            }

          

            foreach(var  pd in playerEntryDic)
            {
               if(pd.Key == num)
                {
                    pd.Value.MasterSet(true);
                }
                else
                {
                    pd.Value.MasterSet(false);
                }


            }



        }

        public void NewPlayerEnter(Player newPlayer)
        {
            PlayerEntry peGo = Instantiate(playerEntry);
            peGo.transform.parent = content;
            peGo.GetComponent<RectTransform>().localScale = Vector2.one;
            peGo.PlayerEntrySet(newPlayer.ActorNumber, newPlayer.NickName, newPlayer);
            playerEntryDic.Add(newPlayer.ActorNumber, peGo);


        }

        public void ExitPlayer(Player otherPlayer)
        {
            if (playerEntryDic.ContainsKey(otherPlayer.ActorNumber)) {

               var go = playerEntryDic[otherPlayer.ActorNumber].gameObject;
               Destroy(go);
               playerEntryDic.Remove(otherPlayer.ActorNumber);
            }
            else
            {

                Debug.LogError("ExitPlayer : " + "DicError");
            }
            ReadyCheck();
        }

        public void PlayerUpdate(Player target , ExitGames.Client.Photon.Hashtable changedProps)
        {
            if (!changedProps.ContainsKey(ConfigData.READY))
            {
                Debug.LogError("PlayerUpdate : Error");
                return;
            }


            if (!playerEntryDic.ContainsKey(target.ActorNumber))
            {
                Debug.LogError("PlayerUpdate : Error");
                return;
            }

            bool ready = (bool)changedProps[ConfigData.READY];

            playerEntryDic[target.ActorNumber].ReadySet(ready);
            ReadyCheck();
        }


        
        private void PlayerEntryDicAllDelete()
        {
           foreach(var  pd in playerEntryDic)
            {
                Destroy(pd.Value.gameObject);
            }


            playerEntryDic.Clear();

        }

        private void ReadyCheck()
        {
            int count = 0;
            foreach(Player player  in PhotonNetwork.PlayerList)
            {
               var prop = player.CustomProperties;
               
                if (!prop.ContainsKey(ConfigData.READY))
                {
                    Debug.LogError(" ReadyPlayer Error : ");
                    return;
                }

                bool isReady = (bool)prop[ConfigData.READY];

                if (isReady)
                {
                    count++;
                }

               
            }

            bool check = false;

            if(count == PhotonNetwork.PlayerList.Length && PhotonNetwork.LocalPlayer.IsMasterClient)
            {

                check = true;
            }
            

            buttonGameStart.gameObject.SetActive(check);

        }



        //ClickEvents

        private void OnCilckExitRoom()
        {
            PhotonNetwork.LeaveRoom();
            
        }

        private void OnClickReadyButton()
        {
            int localNum = PhotonNetwork.LocalPlayer.ActorNumber;
            if (!playerEntryDic.ContainsKey(localNum))
            {
                Debug.LogError("ReadyButtonCilck Error");
                return;
            }

            playerEntryDic[localNum].isReady = !playerEntryDic[localNum].isReady;
            bool isready = playerEntryDic[localNum].isReady;
            ExitGames.Client.Photon.Hashtable properties 
                = new ExitGames.Client.Photon.Hashtable() {
                    { ConfigData.READY , isready } };


            playerEntryDic[localNum].ReadySet(isready);

            PhotonNetwork.LocalPlayer.SetCustomProperties(properties);

            ReadyCheck();
        }

        private void OnClickGameStart()
        {


        }

    }


  
}
