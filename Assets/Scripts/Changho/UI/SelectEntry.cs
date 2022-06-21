using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

using Changho.Data;


namespace Changho.UI
{
    public class SelectEntry : MonoBehaviour
    {
       
        public Button selectButton;

        public CharacterType type;

        public Text selectTex;

        [SerializeField]
        private Transform characterTransform;
        
        private void Start()
        {

            selectButton.onClick.AddListener(OnClickSelect);
            CreateCharacter();

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


        public void CreateCharacter()
        {


            

            CharaterInfo[] charaterInfos =  Resources.LoadAll<CharaterInfo>("Changho/Prefaps/Characters");


            Debug.Log(charaterInfos.Length);

          foreach(var characterInfo in charaterInfos)
            {
                if(characterInfo.characterType == type)
                {
                    
                   GameObject obj  = Instantiate(characterInfo).gameObject;
                   obj.GetComponent<Rigidbody>().useGravity = false;
                    obj.GetComponent<CapsuleCollider>().enabled = false;
                   obj.transform.parent = characterTransform;
                   obj.transform.localPosition = Vector3.zero;
                   obj.transform.localScale = Vector3.one;
                   obj.transform.localRotation = Quaternion.Euler(0, 0, 0);
                   obj.layer = 5;
                   break;
                }


            }


        }


    }
}
