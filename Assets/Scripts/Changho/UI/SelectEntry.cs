using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
namespace Changho.UI
{
    public class SelectEntry : MonoBehaviour
    {
       
        public Button selectButton;

        public CharacterType type;

        public Text selectTex;

        private void Start()
        {

            selectButton.onClick.AddListener(OnClickSelect);

        }

        private void OnClickSelect()
        {

            var entrys =  FindObjectOfType<CharacterSelectPopup>().selectEntrys;


            for(int i = 0; i < entrys.Count; i++)
            {
                if(entrys[i].type != type)
                {
                    entrys[i].selectTex.gameObject.SetActive(false);
                }
                
            }


            selectTex.gameObject.SetActive(true);


            ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable();

            props.Add(ConfigData.READY, false);
            props.Add(ConfigData.CHARACTER, type);

            PhotonNetwork.LocalPlayer.SetCustomProperties(props);


       
        }

    }
}
