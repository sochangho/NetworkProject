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

        public RoomEntry roomEntry;
        public Dictionary<string,RoomEntry> roomEntryDic = new Dictionary<string, RoomEntry>();

        //Buttons
        [SerializeField]
        private Button exitBut;
        [SerializeField]
        private Button randomBut;
        [SerializeField]
        private Button createRoomBut;


        // 
        [SerializeField]
        public CrearteRoomPopup crearteRoomPopup;

        public void Start()
        {
            lobbyManager = LobbyManager.Instance;

            exitBut.onClick.AddListener(lobbyManager.LobbyExit);
            randomBut.onClick.AddListener(lobbyManager.RandomMatch);
            createRoomBut.onClick.AddListener(OnCreateRoomClick);

        }

        public void OnSelectClick()
        {
            
        }
        public void OnCreateRoomClick()
        {
            CrearteRoomPopup crpGo = Instantiate(crearteRoomPopup);
            crpGo.transform.parent = FindObjectOfType<Canvas>().transform;
            crpGo.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
           
        }

        public void ButtonsInteractable(bool value)
        {
            exitBut.interactable = value;                  
            randomBut.interactable = value;
            createRoomBut.interactable = value;
        }
    }
}