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
        public Text playerText;

        public void Start()
        {
            transform.SetParent(KillManager.Instance.rankingPanel.transform);
            SetNickName(photonView.Owner.NickName);
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-35, -30 * (photonView.Owner.GetPlayerNumber() + 1));
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(kill);
            }
            else { 
            kill= (int)stream.ReceiveNext();
            }
        }

        public void test() {
                photonView.RPC("RankUpdate", RpcTarget.All);
        }
        [PunRPC]
        void RankUpdate()
        {
            kill++;
            playerText.text = kill.ToString();
            Debug.Log("RPC");
            //점수가 가장 높은 순서대로 정렬.
            //UI에 반영
        }
        public void SetNickName(string text)
        {
            playerText.text = text;
        }
    }
}