using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

using Changho.UI;


namespace Changho.Managers
{
    public class NetGameManager : GamePhotonManager<NetGameManager>
    {
        public int count;
        public int gamePlayTime;

        public List<Transform> characterSpwanList;


        [SerializeField]
        private Timer timer;

        [SerializeField]
        private GameEndPopup endPopup;

        [SerializeField]
        private Transform createParent;

        private void Start()
        {
           
            GameOwnPlayerInit();
        }



        #region PhotonCallBacks

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
        {

            if (changedProps.ContainsKey(ConfigData.LOAD))
            {
                if ((bool)changedProps[ConfigData.LOAD])
                {
                    GameStartPlayerLoad();
                }
                else
                {
                    GameEndPlayerLoad();
                }
            }
            else if (changedProps.ContainsKey(ConfigData.Exit))
            {
                if ((bool)changedProps[ConfigData.Exit])
                {
                    if (CheckAllExitLevel())
                    {

                        //로드 룸씬;
                        Debug.Log("룸  GO GO");
                    }
                }

            }
        }

        #endregion



        IEnumerator CountBeforeGameStart()
        {
            int time = -1;
            WaitForSeconds wait = new WaitForSeconds(1.0f);

            while(time < count)
            {

                time += 1;

                timer.BeforeGameTextSet(time.ToString());

                yield return wait;

            }

            CreateCharacters();

        }

        IEnumerator GamePlayTimeRoutin()
        {

            int time = -1;

            WaitForSeconds wait = new WaitForSeconds(1.0f);


            while(time < gamePlayTime)
            {
                time += 1;


                timer.GameCountDownTextSet(time, gamePlayTime);
                yield return wait;

            }

            //게임 종료 

            LoadPropertiesSet(false);
        }



        public void GameOwnPlayerInit()
        {

            LoadPropertiesSet(true);
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
            StartCoroutine(GamePlayTimeRoutin());

        }


        private bool CheckAllPlayerLoadLevel()
        {
            return CountPlayer(ConfigData.LOAD) == PhotonNetwork.PlayerList.Length;
        }

        private bool CheckAllPlayerEndLoadLevel()
        {

            return CountPlayer(ConfigData.LOAD) == 0;
        }

        private bool CheckAllExitLevel()
        {
            return CountPlayer(ConfigData.Exit) == PhotonNetwork.PlayerList.Length;
        }

        private int CountPlayer(string key )
        {
            int count = 0;
            for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                object value; 
                if(PhotonNetwork.PlayerList[i].CustomProperties.TryGetValue(key , out value))
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


        private void LoadPropertiesSet(bool value)
        {

            var localProps = PhotonNetwork.LocalPlayer.CustomProperties;
            ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable();

            props.Add(ConfigData.CHARACTER, localProps[ConfigData.CHARACTER]);
            props.Add(ConfigData.LOAD, value);

            PhotonNetwork.LocalPlayer.SetCustomProperties(props);

        }

        private void ExitPropertesSet()
        {
            var localProps = PhotonNetwork.LocalPlayer.CustomProperties;
            ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable();

            props.Add(ConfigData.CHARACTER, localProps[ConfigData.CHARACTER]);
            props.Add(ConfigData.Exit, true);
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        }


        private void GameStartPlayerLoad()
        {

            if (CheckAllPlayerLoadLevel())
            {
                //게임시작
                StartCoroutine(CountBeforeGameStart());
            }
            else
            {

            }
        }

        private void GameEndPlayerLoad()
        {

            if (CheckAllPlayerEndLoadLevel())
            {
               GameEndPopup popup = Instantiate(endPopup);
               popup.transform.parent = createParent;
               popup.transform.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
               popup.onClickExitEventAction = ExitPropertesSet;
               popup.OnOpen();
             
            }

        }




    }
}
