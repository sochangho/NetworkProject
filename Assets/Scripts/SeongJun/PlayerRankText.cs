using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine.UI;
namespace SeongJun { 
    public class PlayerRankText : MonoBehaviourPun, IPunObservable
    {
        IEnumerator stop;
        public int kill;
        public int ranking;
        public Sprite[] backGroundColor;
        public GameObject backGround;
        public Text playerNameText;
        public Text killText;
        public RectTransform RT;
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(kill);
                stream.SendNext(ranking);
            }
            else
            {
                kill = (int)stream.ReceiveNext();
                ranking = (int)stream.ReceiveNext();
            }
        }
        public void Start()
        {   
            //객체 참조
            RT=gameObject.GetComponent<RectTransform>();

            //이 오브젝트를 killManager의 rankingPanel오브젝트의 자식으로 설정한다.
            transform.SetParent(KillManager.Instance.rankingPanel.transform);

            //SetNickName함수를 사용해 플레이어의 이름을 UI에 적용시킨다.
            SetText(photonView.Owner.NickName);

            //플레이어들이 들어온 순서대로 랭킹을 부여한다.
            ranking = photonView.Owner.GetPlayerNumber();

            //이 오브젝트의 위치를 랭킹에 맞게 설정한다.
            transform.localScale = Vector3.one;
            RT.anchoredPosition = new Vector3(-40, (-30 * ranking) - 15 ,0);

            //자신의 오브젝트는 항상 앞에서 보이게
            gameObject.transform.SetAsFirstSibling();
        }
        public void KillUp() {
            photonView.RPC("KillUpRPC", RpcTarget.All);
        }
        [PunRPC]
        public void KillUpRPC() {
            kill++;
            killText.text = kill.ToString();
        }
        public void RankUpdate()
        {
            stop = Move();
            photonView.RPC("RankUpdateRPC", RpcTarget.All);
        }
        [PunRPC]
        public void RankUpdateRPC()
        {
            StopCoroutine(stop);
            StartCoroutine(Move());
        }
        //순위가 상승할때 호출하는 MoveUP함수와 그 반대인 MoveDown함수를 따로 만드는게 어떨까? <-이러면 순위가 자주 바뀌거나 단숨에 오를때 충돌을 일으킬 가능성이 있음. 한곳에서 관리하게 하자.
       IEnumerator Move()
        {
            //스피드를 점점 감소시키는건 어떨까?
            float speed = 30;
            float y = RT.anchoredPosition.y;
            //프레임에 따라서 가끔 값이 목표치보다 더 증가할 때가 있어서 가장 작은수를 저장하고 그 수가 목표치보다 적으면 종료하게 하자.
            float minDistance = 1000;

            /*자신의 위치와 목표위치의 차이가 0.01보다 작으면 종료*/
            while (0.1f <= minDistance)
            {
                if (RT.anchoredPosition.y < (-30 * ranking) - 15)
                {
                    //크기 크게
                    //위로 이동
                    y += speed * 0.01f;
                }
                else
                {
                    y -= speed * 0.01f;
                }

                RT.anchoredPosition = new Vector3(-40, y, 0);
                minDistance = Vector3.Distance(RT.anchoredPosition, new Vector3(-40, (-30 * ranking) - 15, 0));
                yield return null;
            }
            //반복문이 끝나고 약간의 오차가 있을것을 대비하여 자신의 위치를 목표위치로 설정
            RT.anchoredPosition = new Vector3(-40, (-30 * ranking) - 15, 0);
        }

        public void SetText(string name)
        {
            //이름을 설정한대로 적용
            playerNameText.text = name;
            //킬은 0으로 초기화
            killText.text ="0";
            //플레이어가 들어온 순서대로 색깔이 다른 이미지를 적용함.
            backGround.GetComponent<Image>().sprite = backGroundColor[photonView.Owner.GetPlayerNumber()];
            //마지막으로 killManager의 딕셔너리에 이 오브젝트의 정보를 추가함.
            KillManager.Instance.playerRankingDictionary.Add(photonView.Owner.GetPlayerNumber(), this);
        }
    }
}