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
        public Text playerText;
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
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -30 * (ranking + 1), 0);
        }
        IEnumerator move() {

            yield return new WaitForSeconds(0.01f);
        }
        public void SetNickName(string name,int kill)
        {
            playerText.text = name;
            killText.text = kill.ToString();
            KillManager.Instance.playerList.Add(gameObject);
        }
    }
}