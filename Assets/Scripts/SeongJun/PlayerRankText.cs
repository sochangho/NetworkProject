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
            //��ü ����
            RT=gameObject.GetComponent<RectTransform>();

            //�� ������Ʈ�� killManager�� rankingPanel������Ʈ�� �ڽ����� �����Ѵ�.
            transform.SetParent(KillManager.Instance.rankingPanel.transform);

            //SetNickName�Լ��� ����� �÷��̾��� �̸��� UI�� �����Ų��.
            SetText(photonView.Owner.NickName);

            //�÷��̾���� ���� ������� ��ŷ�� �ο��Ѵ�.
            ranking = photonView.Owner.GetPlayerNumber();

            //�� ������Ʈ�� ��ġ�� ��ŷ�� �°� �����Ѵ�.
            transform.localScale = Vector3.one;
            RT.anchoredPosition = new Vector3(-40, (-30 * ranking) - 15 ,0);

            //�ڽ��� ������Ʈ�� �׻� �տ��� ���̰�
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
        //������ ����Ҷ� ȣ���ϴ� MoveUP�Լ��� �� �ݴ��� MoveDown�Լ��� ���� ����°� ���? <-�̷��� ������ ���� �ٲ�ų� �ܼ��� ������ �浹�� ����ų ���ɼ��� ����. �Ѱ����� �����ϰ� ����.
       IEnumerator Move()
        {
            //���ǵ带 ���� ���ҽ�Ű�°� ���?
            float speed = 30;
            float y = RT.anchoredPosition.y;
            //�����ӿ� ���� ���� ���� ��ǥġ���� �� ������ ���� �־ ���� �������� �����ϰ� �� ���� ��ǥġ���� ������ �����ϰ� ����.
            float minDistance = 1000;

            /*�ڽ��� ��ġ�� ��ǥ��ġ�� ���̰� 0.01���� ������ ����*/
            while (0.1f <= minDistance)
            {
                if (RT.anchoredPosition.y < (-30 * ranking) - 15)
                {
                    //ũ�� ũ��
                    //���� �̵�
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
            //�ݺ����� ������ �ణ�� ������ �������� ����Ͽ� �ڽ��� ��ġ�� ��ǥ��ġ�� ����
            RT.anchoredPosition = new Vector3(-40, (-30 * ranking) - 15, 0);
        }

        public void SetText(string name)
        {
            //�̸��� �����Ѵ�� ����
            playerNameText.text = name;
            //ų�� 0���� �ʱ�ȭ
            killText.text ="0";
            //�÷��̾ ���� ������� ������ �ٸ� �̹����� ������.
            backGround.GetComponent<Image>().sprite = backGroundColor[photonView.Owner.GetPlayerNumber()];
            //���������� killManager�� ��ųʸ��� �� ������Ʈ�� ������ �߰���.
            KillManager.Instance.playerRankingDictionary.Add(photonView.Owner.GetPlayerNumber(), this);
        }
    }
}