using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
namespace SeongJun
{
    public class KillManager : MonoBehaviourPun
    {
        public static KillManager Instance { get; private set; }

        //랭킹 판넬
        public GameObject rankingPanel;

        //플레이어이름UI
        public GameObject playerNamePrefeb;
        
        //플레이어 딕셔너리 <플레이어 번호, 플레이어 정보>
        public Dictionary<int, PlayerRankText> playerRankingDictionary;

        //플레이어들의 킬을 담을 리스트
        public List<int> killList = new List<int>();


      

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            if (playerRankingDictionary == null)
            {
                playerRankingDictionary = new Dictionary<int, PlayerRankText>(); 
            }
            PhotonNetwork.Instantiate("SeongJun/playerNamePrefeb", Vector3.zero, Quaternion.identity);
        }
        public void RankCheck()
        {
            photonView.RPC("RankCheckRPC", RpcTarget.All);
        }
        [PunRPC]
        void RankCheckRPC()
        {
            //리스트에 킬 추가
            for (int i = 0; i < playerRankingDictionary.Count; i++)
            {
                killList.Add(playerRankingDictionary[i].kill);
            }





            //가장 킬이 많은 순서대로 삽입.
            for (int j = 0; j < killList.Count; j++)
            {
                int maxkill = -1;
                int playerNumber = -1;
                for (int k = 0; k < killList.Count; k++)
                {
                    if (killList[k] > maxkill)
                    {
                        maxkill = killList[k];
                        playerNumber = k;
                    }
                }
                //플레이어에게 랭킹 부여
                playerRankingDictionary[playerNumber].ranking = j;
                playerRankingDictionary[playerNumber].RankUpdate();
                killList[playerNumber] = -1;
            }

            //랭킹, 킬리스트 초기화
            killList.Clear();
        }
    }
    


}
