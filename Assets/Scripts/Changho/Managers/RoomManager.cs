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




        #endregion
    }
}
