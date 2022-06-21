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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //코드 합칠때 이거만 플레이어가 공격해서 다른 플레이어가 맞았을때 실행되게 하면 될듯?
                playerList[0].gameObject.GetComponent<TestPlayer>().test();
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