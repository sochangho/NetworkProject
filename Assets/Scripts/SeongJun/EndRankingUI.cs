using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
namespace SeongJun { 
public class EndRankingUI : MonoBehaviourPun
    {
    public GameObject GameEndPopUp;
        public GameObject Content;

        private void Start()
        {
            Invoke("ActiveGameEndPopUp",2);
        }
        public void ActiveGameEndPopUp() {
        GameEndPopUp.SetActive(true);
            for (int i = 0; i < KillManager.Instance.playerRankingDictionary.Count; i++)
            {
                for (int j = 0; j < KillManager.Instance.playerRankingDictionary.Count; j++)
                {
                    //��ŷ�� 0�� ������� �����Ѵ�.
                    if (KillManager.Instance.playerRankingDictionary[j].ranking == i)
                    {
                        GameObject ui = PhotonNetwork.Instantiate("SeongJun/GameEndPlayerRankingUI", Vector3.zero, Quaternion.identity);
                        ui.transform.localScale = Vector3.one;
                        ui.transform.SetParent(Content.transform);
                        ui.GetComponent<GameEndPlayerRankingUI>().textUpdate(KillManager.Instance.playerRankingDictionary[j].photonView.Owner.NickName);
                        //1���̸� �̸����� ���� �޴� Ȱ��ȭ
                        if (KillManager.Instance.playerRankingDictionary[j].ranking == 0) {
                            ui.GetComponent<GameEndPlayerRankingUI>().Medal.SetActive(true) ;
                        }
                    }
                }
            }
    }
}
}