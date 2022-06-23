using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using Changho.Room;


namespace Changho.UI
{

    public class PlayerEntry : MonoBehaviour
    {
        [SerializeField]
        protected Text playerName;

        [SerializeField]
        protected GameObject readyObject;

        [SerializeField]
        protected GameObject masterObject;

        [SerializeField]
        protected RawImage rawImage;

        protected CharactersCamRender.RenderTextureValue textureValue;
     
        protected int ownNumber;

        public int OwnNumber { get { return ownNumber; } }

        public bool isReady;

        public CharacterType characterType;
        public void PlayerEntrySet(int actornumber , string playername , Player player )
        {


            ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
            properties.Add(ConfigData.READY, false);


           var localProperties =  PhotonNetwork.LocalPlayer.CustomProperties;

            if (localProperties.ContainsKey(ConfigData.CHARACTER))
            {
                properties.Add(ConfigData.CHARACTER, localProperties[ConfigData.CHARACTER]);
            }
            else
            {
                properties.Add(ConfigData.CHARACTER, CharacterType.type1);
            }


            player.SetCustomProperties(properties);
            characterType = CharacterType.type1;

            isReady = false;
            this.ownNumber = actornumber;
            playerName.text = playername;
            readyObject.SetActive(false);
            masterObject.SetActive(player.IsMasterClient);
           
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
            object type;
            if(player.CustomProperties.TryGetValue(ConfigData.CHARACTER ,out type))
            {
                characterType = (CharacterType)type;
                
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

        public void CharecterSet(CharactersCamRender.RenderTextureValue textureValue)
        {
            this.textureValue = textureValue;
            rawImage.texture = textureValue.renderTexture;
            
        }

        

        public void DeleteCharacter() => textureValue.Use = false;

        public CharactersCamRender.RenderTextureValue GetRenderTextureValue() { return textureValue; }

    }
}
