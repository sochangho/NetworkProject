using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        private int actornumber;

        public int ActorNumber { get; }


        public void PlayerEntrySet(int actornumber , string playername , bool master , bool ready)
        {
            this.actornumber = actornumber;
            playerName.text = playername;
            readyObject.SetActive(ready);
            masterObject.SetActive(master);
            
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
