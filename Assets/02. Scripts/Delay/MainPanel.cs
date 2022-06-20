using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections.Generic;


public class MainPanel : MonoBehaviourPunCallbacks
{
    [Header("Login")]
    public GameObject loginPanel;
    public InputField playerNameInput;

    [Header("Connect")]
    public GameObject inConnectPanel;

    [Header("Create Room")]
    public GameObject createRoomPanel;

    public InputField roomNameInputField;
    public InputField maxPlayersInputField;

    [Header("Lobby")]
    public GameObject inLobbyPanel;

    public GameObject roomContent;
    public GameObject roomEntryPrefab;

    [Header("Room")]
    public GameObject inRoomPanel;

    public GameObject playerListContent;
    public Button startGameButton;
    public GameObject playerEntryPrefab;


    private Dictionary<string, RoomInfo> cachedRoomList;
    private Dictionary<string, GameObject> roomListEntries;
    private Dictionary<int, GameObject> playerListEntries;
}