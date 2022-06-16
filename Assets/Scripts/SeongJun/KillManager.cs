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
        //순위가 달라지면 서로 위치가 바뀜

        //랭킹 판넬
        public GameObject rankingPanel;
        //플레이어 텍스트 오브젝트
        public GameObject playerNamePrefeb;
        List<GameObject> playerList;
        
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            if (playerList == null)
            {
                playerList = new List<GameObject>();
            }
            //생성될 playerNamePrefeb들의 y값을 조정할 변수
            int y = -30;
            
            GameObject name = PhotonNetwork.Instantiate("playerNamePrefeb",Vector3.zero,Quaternion.identity);
            //name.transform.SetParent(rankingPanel.transform);
            /*TestPlayer player = name.gameObject.GetComponent<TestPlayer>();
            player.SetNickName(PhotonNetwork.LocalPlayer.NickName);
            name.GetComponent<RectTransform>().anchoredPosition = new Vector2(-35,y);
            playerList.Add(name);
            y -= 30;*/
            playerList.Add(name);
            //entry.GetComponent<플레이어>().Initialize(p.번호 또는 순위, p.이름); 플레이어의 정보를 받아와야 한다. 아직은 merge안했으니 패스

        }
        public void IsDead()
        {
            //마지막으로 때린 사람 기억하고.
            //마지막으로 때린 사람의 점수 올리고.
            //killmanager클래스에서 rankupdate함수 실행
            //리스폰
        }
        private void Update()
        {
            if (Input.GetButtonDown("Submit"))
            {
                playerList[0].gameObject.GetComponent<TestPlayer>().test();
                Debug.Log(playerList[0].name);
            }
        }
    }
}