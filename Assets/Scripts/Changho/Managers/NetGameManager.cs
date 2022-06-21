using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

namespace Changho.Managers
{
    public class NetGameManager : GamePhotonManager<NetGameManager>
    {

        public List<Transform> characterSpwanList;

        private void Start()
        {
            Debug.Log("dddddd");
            GameOwnPlayerInit();
        }



        #region PhotonCallBacks

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
        {

            if (changedProps.ContainsKey(ConfigData.LOAD))
            {
                if (CheckAllPlayerLoadLevel())
                {
                   
                    //게임시작
                    CreateCharacters();
                }
                else
                {
                    
                }
            }
        }

        #endregion



        public void GameOwnPlayerInit()
        {

           var localProps = PhotonNetwork.LocalPlayer.CustomProperties;
           ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable();

           props.Add(ConfigData.CHARACTER, localProps[ConfigData.CHARACTER]);
           props.Add(ConfigData.LOAD, true);

           PhotonNetwork.LocalPlayer.SetCustomProperties(props);

         
        }


        private void CreateCharacters()
        {

          string playerName =  PlayerLoad(PhotonNetwork.LocalPlayer);

            if(playerName == null)
            {
                Debug.LogError("이름을 찾을 수 없음");
                return;
            }

            int index = 0;
            for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if(PhotonNetwork.LocalPlayer.ActorNumber == PhotonNetwork.PlayerList[i].ActorNumber)
                {
                    index = i;
                }

            }



            string path = string.Format("Changho/Prefaps/Characters/{0}",playerName);
            PhotonNetwork.Instantiate(path, characterSpwanList[index].position, characterSpwanList[index].rotation);


        }


        private bool CheckAllPlayerLoadLevel()
        {
            return CountPlayer() == PhotonNetwork.PlayerList.Length;
        }

        private int CountPlayer()
        {
            int count = 0;
            for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                object value; 
                if(PhotonNetwork.PlayerList[i].CustomProperties.TryGetValue(ConfigData.LOAD , out value))
                {
                    if ((bool)value)
                    {
                        count++;
                    }
                }
            }


            return count;
        }


        private string PlayerLoad(Player player)
        {

            string playerName = null;

            object type;
           if( player.CustomProperties.TryGetValue(ConfigData.CHARACTER , out type))
            {

                Changho.Data.CharaterInfo[] charaterInfos = Resources.LoadAll<Changho.Data.CharaterInfo>("Changho/Prefaps/Characters");

                foreach(var characterinfo in charaterInfos)
                {

                    if((CharacterType)type == characterinfo.characterType)
                    {
                        playerName = characterinfo.gameObject.name;
                        break;
                    }

                }


              
            }
            

            return playerName;
        }

    }
}
