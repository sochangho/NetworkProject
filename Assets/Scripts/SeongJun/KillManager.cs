using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
namespace SeongJun { 
    public class KillManager : MonoBehaviourPun
    {
        //플레이어들의 정보 받아오기 (이름같은거)
        //각 플레이어의 이름을 TextUI로 생성
        //킬한 사람의 점수를 올리는 함수 생성
        //순위가 달라지면 서로 위치가 바뀜

        //랭킹 판넬
        public GameObject rankingPanel;
        //플레이어 텍스트 오브젝트
        public GameObject playerNamePrefeb;

        //플레이어 순위 정보
        private Dictionary<int, GameObject> playerListEntries;
        private void Start()
        {
            foreach (Player p in PhotonNetwork.PlayerList)
            {
                GameObject entry = Instantiate(playerNamePrefeb);
                entry.transform.SetParent(rankingPanel.transform);
                entry.gameObject.GetComponent<Text>().text = p.NickName;
                //entry.gameObject.GetComponent<Text>().color = 플레이어의 색깔;
                //entry.transform.localScale = Vector3.one; 스케일은 조정할 필요가 있나? 나중에 확인
                //entry.GetComponent<플레이어>().Initialize(p.번호 또는 순위, p.이름); 플레이어의 정보를 받아와야 한다. 아직은 merge안했으니 패스
 
                playerListEntries.Add(p.ActorNumber, entry);
            }
            
        }
        [PunRPC]
        public void RankUpdate() {
            //플레이어의 인원수만큼 반복.
            //점수가 가장 높은 순서대로 정렬.
            //UI에 반영
        }
        void IsDead(Player p)
        {
            //높이가 -3보다 낮으면 (번지되는 높이는 맵에 따라 원하는 대로 설정하면 됌. 반드시 -3일 필요는 없음.)
           
                //마지막으로 때린 사람 기억하고.
                //마지막으로 때린 사람의 점수 올리고.
                //killmanager클래스에서 rankupdate함수 실행
                RankUpdate();
                //리스폰
            
        }
    }
}