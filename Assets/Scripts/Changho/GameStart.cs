using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Changho.Managers;


namespace Changho
{
    public class GameStart : MonoBehaviour
    {
        public GameObject readyUI;
        public GameObject startUI;



        public void UiGameStart()
        {

            StartCoroutine(ReadyRoutin());

        }

        IEnumerator ReadyRoutin()
        {

            GameObject ready = Instantiate(readyUI);
            ready.transform.parent = this.transform.parent;
            ready.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
            yield return new WaitForSeconds(1.0f);

            Destroy(ready);

            StartCoroutine(StartRoutin());
        }

        IEnumerator StartRoutin()
        {

            GameObject start = Instantiate(startUI);
            start.transform.parent = this.transform.parent;
            start.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);

            yield return new WaitForSeconds(1.0f);

            Destroy(start);


            NetGameManager.Instance.PlayersIsContoller(true);
            NetGameManager.Instance.GamePlayTimeRoutinStart();

        }



    }
}