using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public GameObject loading;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Image progressBar;

    private void Start()
    {
        loading.gameObject.SetActive(true);
        StartCoroutine(CLoading());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CLoading()
    {
        progressBar.fillAmount = 0f;
        
        //yield return StartCoroutine(Fade(true));
        //yield return new WaitForSeconds(1.0f);      
        yield return StartCoroutine(CLoadingFill(4f));     
        
               
    }

    private IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;
        while(timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 5f;
            canvasGroup.alpha = isFadeIn ? Mathf.Lerp(0f, 1f, timer):Mathf.Lerp(1f, 0f, timer);
        }

        if(!isFadeIn)
        {
            gameObject.SetActive(false);
        }

        Changho.Managers.NetGameManager.Instance.gameStart.UiGameStart();
    }

    private IEnumerator CLoadingFill(float limit)
    {
        float timer = 0f;
        while(timer <= limit)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 1f;
            progressBar.fillAmount = Mathf.Lerp(0f, 1f, timer / 4f);
        }
        yield return StartCoroutine(Fade(false));
    }

}
