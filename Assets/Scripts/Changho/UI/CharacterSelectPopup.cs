using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Chat;
using Photon.Pun;

namespace Changho.UI
{
    public class CharacterSelectPopup : Popup
    {
        public List<SelectEntry> selectEntrys;


        private void Awake()
        {
            
            //for(int i = 0; i < selectEntrys.Count; i++)
            //{
            //    selectEntrys[i].selectButton.onClick.AddListener(()=> {

            //        selectEntrys[i].selectTex.gameObject.SetActive(false);
                
            //    });

            //}


        }

        private void Start()
        {

          var localProps =  PhotonNetwork.LocalPlayer.CustomProperties;

          CharacterType type =  (CharacterType)localProps[ConfigData.CHARACTER];

           for(int i = 0; i < selectEntrys.Count; i++)
            {
                if(selectEntrys[i].type == type)
                {

                    selectEntrys[i].selectTex.gameObject.SetActive(true);

                }
                else
                {
                    selectEntrys[i].selectTex.gameObject.SetActive(false);
                }


            }




        }



        public override void OnClose()
        {
          Room.Room room =  FindObjectOfType<Room.Room>();

            room.buttonExit.interactable = true;
            room.buttonReady.interactable = true;
            room.buttonGameStart.interactable = true;

           base.OnClose();
        }

    }
}