using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
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
            
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
           
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
           
        }


        public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
        {
            base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        }



        #endregion
    }
}
