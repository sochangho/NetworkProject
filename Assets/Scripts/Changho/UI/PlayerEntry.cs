using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;
namespace Changho.UI
{

    public class PlayerEntry : MonoBehaviour
    {
        [SerializeField]
        private Text playerName;

        [SerializeField]
        private GameObject readyObject;

        [SerializeField]
        private GameObject masterObject;

        private int ownNumber;

        public int OwnNumber { get { return ownNumber; } }

        public bool isReady;

        public void PlayerEntrySet(int actornumber , string playername , Player player )
        {


            ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
            properties.Add(ConfigData.READY, false);

            player.SetCustomProperties(properties);

            isReady = false;
            this.ownNumber = actornumber;
            playerName.text = playername;
            readyObject.SetActive(false);
            masterObject.SetActive(player.IsMasterClient);
            Debug.Log("fdfdfdfdff");

        }

        public void LoadPlayerEntry(int actornumber , string playername ,Player player)
        {
            object is_ready;
            if(player.CustomProperties.TryGetValue(ConfigData.READY ,out is_ready))
            {
                this.ownNumber = actornumber;
                playerName.text = playername;
                readyObject.SetActive((bool)is_ready);
               
            }

            masterObject.SetActive(player.IsMasterClient);

        }


        public void MasterSet(bool value)
        {
            masterObject.SetActive(value);
        }

        public void ReadySet(bool value)
        {
            readyObject.SetActive(value);
        }

    }
}
