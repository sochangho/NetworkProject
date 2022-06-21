using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;
namespace SeongJun { 
    public class TestPlayer : MonoBehaviourPun, IPunObservable
    {
        //플레이어 이름 띄우는건 완성 랭크 시스템도 지금은 숫자를 움직이는걸로 했지만 최종적으론 위치값을 움직이게 할거 
        public int kill;
        public int ranking;
        public Text playerNameText;
        public Text killText;
        public void Start()
        {
            //플레이어들이 들어온 순서대로 랭킹을 부여한다.
            ranking = photonView.Owner.GetPlayerNumber();
            //이 오브젝트를 killManager의 rankingPanel오브젝트의 자식으로 설정한다.
            transform.SetParent(KillManager.Instance.rankingPanel.transform);
            //SetNickName함수를 사용해 플레이어의 이름을 UI에 적용시킨다.
            SetNickName(photonView.Owner.NickName,kill);
            //이 오브젝트의 위치를 랭킹에 맞게 설정한다.
            transform.localScale = Vector3.one;
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -30 * (ranking + 1),0);
        }

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

        public void test()
        {
            photonView.RPC("RankUpdate", RpcTarget.All);
        }
        [PunRPC]
        void RankUpdate()
        {
            kill++;
            killText.text = kill.ToString();
            for (int i = 0; i < KillManager.Instance.playerList.Count; i++)
            {
                if (kill > KillManager.Instance.playerList[i].gameObject.GetComponent<TestPlayer>().kill
                   && ranking > KillManager.Instance.playerList[i].gameObject.GetComponent<TestPlayer>().ranking)
                {
                    ranking--;
                    StopCoroutine(MoveUP(i));
                    StartCoroutine(MoveUP(i));
                    KillManager.Instance.playerList[i].gameObject.GetComponent<TestPlayer>().ranking++;
                }
            }
            for (int i = 0; i < KillManager.Instance.playerList.Count; i++)
            {
                KillManager.Instance.playerList[i].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -30 * (KillManager.Instance.playerList[i].gameObject.GetComponent<TestPlayer>().ranking + 1), 0);
            }
        }
        //순위가 상승할때 호출하는 MoveUP함수와 그 반대인 MoveDown함수를 따로 만드는게 어떨까?
        IEnumerator MoveUP(int playerListNumber)
        {
            float speed=3f;
            /*자신의 위치와 목표위치의 차이가 0.1보다 작으면 종료*/
            while (0.1f > Vector3.Distance(transform.position, new Vector3(0, -30 * (KillManager.Instance.playerList[playerListNumber].gameObject.GetComponent<TestPlayer>().ranking + 1))))
            {
                //순서를 가장 앞으로 설정
                transform.SetAsFirstSibling();
                //시작하면 점점 커지다가 목표위치의 반정도 오면 원래대로 돌아가는건 어떨까.
                //자신의 위치를 목표위치로 이동하되 점점 감속함.
                yield return new WaitForSeconds(0.01f);
            }
            //반복문이 종료되면 자신의 위치를 목표위치로 설정
            KillManager.Instance.playerList[playerListNumber].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -30 * (KillManager.Instance.playerList[playerListNumber].gameObject.GetComponent<TestPlayer>().ranking + 1), 0);

        }
        public void SetNickName(string name,int kill)
        {
            playerNameText.text = name;
            killText.text = kill.ToString();
            KillManager.Instance.playerList.Add(gameObject);
        }
    }
}