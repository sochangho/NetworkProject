

using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;




namespace Changho.Managers {
    public class RoomManager : GamePhotonManager<RoomManager>
    {


        public Room.Room room;


        public override void Awake()
        {
            base.Awake();
        }

        #region PhotonCallbacks
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            room.NewPlayerEnter(newPlayer);
        }


        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            room.ExitPlayer(otherPlayer);
        }

        public override void OnLeftRoom()
        {

            PhotonNetwork.LoadLevel("LobbyScene");
            
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            room.ChangedMaster(newMasterClient);
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            room.PlayerUpdate(targetPlayer, changedProps);
        }


        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            room.RoomPropertiesUpdate(propertiesThatChanged);
        }


        #endregion
    }
}
