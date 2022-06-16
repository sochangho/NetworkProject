using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Changho.UI;
using Changho.Managers;

namespace Changho.Room
{
    public class Room : MonoBehaviour
    {
        private RoomManager roomManager;

        [SerializeField]
        private Transform content;


        [SerializeField]
        private PlayerEntry playerEntry;
        private Dictionary<int , PlayerEntry> playerEntryList;


        private void Awake()
        {
            roomManager = RoomManager.Instance;

            roomManager.room = this;

            foreach(Player player in PhotonNetwork.PlayerList)
            {
               PlayerEntry peGo = Instantiate(playerEntry);
               peGo.transform.parent = content;
               peGo.GetComponent<RectTransform>().localScale = Vector2.one;
               object readyValue;
               if(player.CustomProperties.TryGetValue( RoomPlayerData.READY , out readyValue))
               {
                    peGo.PlayerEntrySet(player.ActorNumber, player.NickName, false, (bool)readyValue);    
               }

                playerEntryList.Add(player.ActorNumber, peGo);
            }



        }



        public void NewPlayerEnter(Player newPlayer)
        {

            

        }



    }


    public class RoomPlayerData
    {

        public const string READY = "READY";

        
    }
}
