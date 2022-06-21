using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
namespace SeongJun { 
    public class KillManager : MonoBehaviourPun
    {
        public static KillManager Instance { get; private set; }

        //플레이어들의 정보 받아오기 (이름같은거)
        //각 플레이어의 이름을 TextUI로 생성
        //킬한 사람의 점수를 올리는 함수 생성

        //랭킹 판넬
        public GameObject rankingPanel;
        //플레이어 텍스트 오브젝트
        public GameObject playerNamePrefeb;

        //플레이어 리스트
        public List<GameObject> playerList = new List<GameObject>();
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            GameObject name = PhotonNetwork.Instantiate("playerNamePrefeb",Vector3.zero,Quaternion.identity);
        }
        private void Update()
        {
          //  if (!photonView.IsMine)
          //      return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerList[0].gameObject.GetComponent<TestPlayer>().kill++;
                photonView.RPC("RankingCheck", RpcTarget.All);
                for (int i = 0; i < playerList.Count; i++)
                {
                    playerList[i].gameObject.GetComponent<TestPlayer>().test();
                } 
            }
        }
        [PunRPC]
         void RankingCheck()
        {
            int max = -1;
            for (int i = 0; i < playerList.Count; i++)
            {
                for (int j = 0; j < playerList.Count; j++)
                {
                    if (playerList[i].gameObject.GetComponent<TestPlayer>().ranking > playerList[j].gameObject.GetComponent<TestPlayer>().ranking&& playerList[i].gameObject.GetComponent<TestPlayer>().kill > playerList[j].gameObject.GetComponent<TestPlayer>().kill)
                    {
                        playerList[i].gameObject.GetComponent<TestPlayer>().ranking--;
                        playerList[j].gameObject.GetComponent<TestPlayer>().ranking++;
                    }
                }
            }
        }
        public void IsDead()
        {
            //마지막으로 때린 사람 기억하고.
            //마지막으로 때린 사람의 점수 올리고.
            //killmanager클래스에서 rankupdate함수 실행
            //리스폰
        }
     
    }
}