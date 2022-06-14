using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Changho.UI {


    public class InfoText : MonoBehaviour
    {
        [SerializeField]
        private Text infoText;
        private CanvasGroup canvasGroup;

        public void InfoTextSet(string text)
        {
            infoText.text = text;
            canvasGroup = GetComponent<CanvasGroup>();
            StartCoroutine(FadeOut());
        }


        IEnumerator FadeOut()
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= 0.001f;
                yield return null;
            }

            Destroy(gameObject);
        }

    }
}
