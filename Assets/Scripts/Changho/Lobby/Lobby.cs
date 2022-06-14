using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Changho.Managers;
using Changho.UI;

namespace Changho.Lobby
{
    public class Lobby : MonoBehaviour
    {
        private LobbyManager lobbyManager;

        public LobbyEntry lobbyEntry;
        public List<LobbyEntry> lobbyEntrys;

        public Button exitBut;
        public Button selectBut;
        public Button randomBut;
        public Button createRoomBut;
        
        public void Start()
        {
            lobbyManager = LobbyManager.Instance;

            exitBut.onClick.AddListener(lobbyManager.LobbyExit);
            selectBut.onClick.AddListener(OnSelectClick);
            randomBut.onClick.AddListener(lobbyManager.RandomMatch);
            createRoomBut.onClick.AddListener(OnCreateRoomClick);

        }

        public void OnSelectClick()
        {

        }
        public void OnCreateRoomClick()
        {


        }


    }
}