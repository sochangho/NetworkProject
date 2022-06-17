using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine;
using Changho.UI;


namespace Changho.Managers
{
    public class LobbyManager : GamePhotonManager<LobbyManager>
    {
        private Lobby.Lobby lobby;

        private const string ROOM_NAME = "RoomName";

        #region UnityEvents
        public override void Awake()
        {
            base.Awake();
            lobby = FindObjectOfType<Lobby.Lobby>();
            
        }

        public void Start()
        {
           
        }

        #endregion


        #region PhotonCallbacks

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {

            Debug.Log(PhotonNetwork.LocalPlayer.NickName + " : JoinRoom");
            

        }


        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("접속종료 " + cause);

            TransitionScene("LoginScene");

        }

        public override void OnJoinedRoom()
        {
            Debug.Log("방 접속 ");
            TransitionScene("RoomScene");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.LogError("접속 실패 방 생성");

            byte maxplayers = 4;
            CreateRoom(PhotonNetwork.LocalPlayer.NickName + "'s Game", maxplayers);
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            Debug.Log("로비 업데이트");
            List<LobbyEntry> lobbyEntrys = lobby.lobbyEntrys;

            for(int i = 0; i < lobbyEntrys.Count; i++)
            {
                Destroy(lobbyEntrys[i].gameObject);

            }
            lobbyEntrys.Clear();


            foreach(var room in roomList)
            {
                if (!room.IsOpen || !room.IsVisible)
                {
                    continue;
                }

                var lobbyGo = Instantiate(lobby.lobbyEntry);
                lobbyGo.transform.parent = lobby.transform;

                string roomName = (string)room.CustomProperties[ROOM_NAME];
                string players = string.Format("{0} / {1}", room.PlayerCount, room.MaxPlayers);


                lobbyGo.EntrySetting(roomName, players, () => {

                    if(room.PlayerCount < room.MaxPlayers)
                    {
                        PhotonNetwork.LeaveLobby();
                        PhotonNetwork.JoinRoom(room.Name);
                    }

                   
                });
            }




        }


        public override void OnLeftLobby()
        {
            Debug.Log("룸 나가기");
        }

        #endregion

        public void CreateRoom(string roomName , byte maxPlayer)
        {
            ExitGames.Client.Photon.Hashtable hashTable = new ExitGames.Client.Photon.Hashtable();
            hashTable.Add(ROOM_NAME, roomName);
            string[] propertiesListedInLobby = new string[1];
            propertiesListedInLobby[0] = ROOM_NAME;


            PhotonNetwork.CreateRoom(PhotonNetwork.LocalPlayer.UserId + "_" + System.DateTime.UtcNow.ToFileTime(),
            new RoomOptions
            {
                MaxPlayers = maxPlayer,
                IsVisible = true,
                IsOpen = true,
                CustomRoomProperties = hashTable,
                CustomRoomPropertiesForLobby = propertiesListedInLobby
            });


        }

        public void RandomMatch()
        {
            PhotonNetwork.LeaveLobby();
            PhotonNetwork.JoinRandomRoom();

        }


        public void LobbyExit()
        {
            PhotonNetwork.LeaveLobby();
            PhotonNetwork.Disconnect();
            
        }


    }
}