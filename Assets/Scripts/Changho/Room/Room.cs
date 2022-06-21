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

        public Button buttonGameStart;


        public Button buttonReady;


        public Button buttonExit;
        //--------------------------------------------------


        [SerializeField]
        private Transform content;


        [SerializeField]
        private PlayerEntry playerEntry;

        [SerializeField]
        private PlayerOwnEntry ownEntry;




        private Dictionary<int, PlayerEntry> playerEntryDic = new Dictionary<int, PlayerEntry>();



        private void Awake()
        {
            roomManager = RoomManager.Instance;

            roomManager.room = this;

            foreach (Player player in PhotonNetwork.PlayerList)
            {


                if (player.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    ownEntry.PlayerEntrySet(player.ActorNumber, player.NickName, player);

                }
                else
                {
                    PlayerEntry peGo = Instantiate(playerEntry);
                    peGo.transform.parent = content;
                    peGo.GetComponent<RectTransform>().localScale = Vector2.one;
                    peGo.LoadPlayerEntry(player.ActorNumber, player.NickName, player);
                    playerEntryDic.Add(player.ActorNumber, peGo);
                }


            }



            CharactersCamRender.Instance.CharaterInit(playerEntryDic);

            buttonGameStart.onClick.AddListener(OnClickGameStart);
            buttonReady.onClick.AddListener(OnClickReadyButton);
            buttonExit.onClick.AddListener(OnCilckExitRoom);

        }

        public void ChangedMaster(Player newMasterClient)
        {

            int num = newMasterClient.ActorNumber;

            if ((newMasterClient.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber) && !playerEntryDic.ContainsKey(num))
            {
                Debug.LogError("ChangedMaster Error");
                return;
            }


            if (PhotonNetwork.LocalPlayer.ActorNumber == num)
            {
                ownEntry.MasterSet(true);

                foreach (var pd in playerEntryDic)
                {
                    pd.Value.MasterSet(false);
                }
                return;

            }







            foreach (var pd in playerEntryDic)
            {
                if (pd.Key == num)
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
            CharactersCamRender.Instance.CharacterAdd(peGo);

        }

        public void ExitPlayer(Player otherPlayer)
        {
            if (playerEntryDic.ContainsKey(otherPlayer.ActorNumber))
            {

                var go = playerEntryDic[otherPlayer.ActorNumber].gameObject;
                CharactersCamRender.Instance.CharacterDelete(go.GetComponent<PlayerEntry>());
                Destroy(go);
                playerEntryDic.Remove(otherPlayer.ActorNumber);
            }
            else
            {

                Debug.LogError("ExitPlayer : " + "DicError");
            }
            ReadyCheck();
        }

        // 여기서 부터......
        public void PlayerUpdate(Player target, ExitGames.Client.Photon.Hashtable changedProps)
        {


            if ((PhotonNetwork.LocalPlayer.ActorNumber != target.ActorNumber) && !playerEntryDic.ContainsKey(target.ActorNumber))
            {
                Debug.LogError("PlayerUpdate : Error");
                return;
            }

            ReadyUpdate(target, changedProps);
            CharacterUpdate(target, changedProps);

        }

        private void CharacterUpdate(Player target, ExitGames.Client.Photon.Hashtable changedProps)
        {

            if (changedProps.ContainsKey(ConfigData.CHARACTER))
            {
                CharacterType type = (CharacterType)changedProps[ConfigData.CHARACTER];


                if (target.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    playerEntryDic[target.ActorNumber].characterType = type;
                    CharactersCamRender.Instance.CharacterChange(playerEntryDic[target.ActorNumber]);
                }
                else
                {
                    ownEntry.characterType = type;
                    CharactersCamRender.Instance.CharacterChange(ownEntry);
                }


            }

        }

        private void ReadyUpdate(Player target, ExitGames.Client.Photon.Hashtable changedProps)
        {

            if (changedProps.ContainsKey(ConfigData.READY))
            {
                bool ready = (bool)changedProps[ConfigData.READY];

                if (target.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    playerEntryDic[target.ActorNumber].ReadySet(ready);
                }
                else
                {
                    ownEntry.ReadySet(ready);
                }
                ReadyCheck();
            }
        }



        private void PlayerEntryDicAllDelete()
        {
            foreach (var pd in playerEntryDic)
            {
                Destroy(pd.Value.gameObject);
            }


            playerEntryDic.Clear();

        }

        private void ReadyCheck()
        {
            int count = 0;



            foreach (Player player in PhotonNetwork.PlayerList)
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

            if (count == PhotonNetwork.PlayerList.Length && PhotonNetwork.LocalPlayer.IsMasterClient && count > 1)
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


            ownEntry.isReady = !ownEntry.isReady;

            bool ownReady = ownEntry.isReady;

            ExitGames.Client.Photon.Hashtable properties
                = new ExitGames.Client.Photon.Hashtable() {
                    { ConfigData.READY , ownReady } };


            ownEntry.ReadySet(ownReady);

            PhotonNetwork.LocalPlayer.SetCustomProperties(properties);

            ReadyCheck();
        }

        private void OnClickGameStart()
        {

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;


            PhotonNetwork.LoadLevel("ChanghoMap_Cube 1");


        }

    }



}
