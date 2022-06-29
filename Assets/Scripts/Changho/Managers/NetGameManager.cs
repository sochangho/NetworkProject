using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Realtime;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

using Changho.UI;

using SeongJun;
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

        [SerializeField]
        private PhotonView owntargetObject;

        

        public override void Awake()
        {
           base.Awake();
            GameOwnPlayerInit();
            gamePlayTime = 3;
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

        public void UpScore(PhotonView ownPhotonView , int oponentNumber)
        {

            if (!ownPhotonView.IsMine)
            {
                return;
            }

            int num = ownPhotonView.Owner.GetPlayerNumber();
            KillManager.Instance.playerRankingDictionary[num].KillUp();
            KillManager.Instance.RankCheck();
        }

        public void RespwanRandomPosSet(GameObject respwanObj)
        {
            int randomIndex = Random.Range(0, characterSpwanList.Count);
            respwanObj.transform.position = characterSpwanList[randomIndex].position;
            respwanObj.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        
        public void Respwan(int playerActorNumber)
        {
          
            StartCoroutine(SecondAfterRespwan(playerActorNumber, 2));
        }

        IEnumerator SecondAfterRespwan(int playerActorNumber , float second)
        {
            yield return new WaitForSeconds(second);

            for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if(playerActorNumber == PhotonNetwork.PlayerList[i].ActorNumber)
                {

                    string playerName = PlayerLoad(PhotonNetwork.PlayerList[i]);

                    int index = Random.Range(0, characterSpwanList.Count);

                   
                    string path = string.Format("Changho/Prefaps/Characters/{0}", playerName);

                    //GameObject playerObj = Resources.Load<GameObject>(path);

                    var go =PhotonNetwork.Instantiate(path, characterSpwanList[index].position, characterSpwanList[index].rotation);

                    if (PhotonNetwork.PlayerList[i].ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
                    {
                        owntargetObject = go.GetComponent<PhotonView>();
                        owntargetObject.GetComponent<PlayerController>().number = PhotonNetwork.LocalPlayer.ActorNumber;
                        Camera.main.gameObject.GetComponent<Changho.PlayerCameraSet>().CameraFollow();
                    }


                    break;
                }
                
            }
            
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





            if (owntargetObject == null)
            {

                string path = string.Format("Changho/Prefaps/Characters/{0}", playerName);
                var go = PhotonNetwork.Instantiate(path, characterSpwanList[index].position, characterSpwanList[index].rotation);

                owntargetObject = go.GetComponent<PhotonView>();
                Camera.main.gameObject.AddComponent<Changho.PlayerCameraSet>().CameraFollow();
                owntargetObject.GetComponent<PlayerController>().number = PhotonNetwork.LocalPlayer.ActorNumber;
            }
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

        private bool CheckAllPlayerExit()
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
            Debug.Log("생성 : " + value);
            var localProps = PhotonNetwork.LocalPlayer.CustomProperties;
            ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable();

            props.Add(ConfigData.CHARACTER, localProps[ConfigData.CHARACTER]);
            
            props.Add(ConfigData.LOAD, value);

            PhotonNetwork.LocalPlayer.SetCustomProperties(props);

        }


        private void MasterClientExit()
        {
            
            if (PhotonNetwork.LocalPlayer.IsMasterClient)
            {

                if (!PhotonNetwork.CurrentRoom.IsOpen)
                {
                    PhotonNetwork.CurrentRoom.IsOpen = true;

                }

                if (!PhotonNetwork.CurrentRoom.IsVisible)
                {

                    PhotonNetwork.CurrentRoom.IsVisible = true;
                }

                Debug.Log("마스터 클라이언트 ");
                PhotonNetwork.DestroyAll();
                TransitionScene("RoomScene");
            }
         



        }


        private void GameStartPlayerLoad()
        {

            if (CheckAllPlayerLoadLevel())
            {

                Debug.Log("플레이어 수 " + PhotonNetwork.PlayerList.Length);
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
               popup.onClickExitEventAction = MasterClientExit;
               
             
            }

        }




    }
}
