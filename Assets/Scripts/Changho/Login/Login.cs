using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Changho.Managers;
using Changho.Interface;
using Changho.UI;
namespace Changho.Login
{

    public class Login : MonoBehaviour , Iinfomation
    {
        [SerializeField]
        private InputField inputField;
        [SerializeField]
        private Button loginButton;
        [SerializeField]
        private InfoText infoText;
        [SerializeField]
        private Text serverinfoText;



        [SerializeField]
        private Transform perantTranform;

        private InfoText cloneInfoText;

        private LoginSceneManager loginSceneManager;

        public void Start()
        {
            loginSceneManager = LoginSceneManager.Instance;
            loginButton.onClick.AddListener(OnLoginButtonClick);

        }

        public void OnLoginButtonClick()
        {
            if(inputField.text == "")
            {
                Infomation("Enter Name....");
                Debug.LogError("이름 입력해주세요");
                return;
            }
            


            loginSceneManager.Connect(inputField.text);
           
        }


        public void Infomation(string str)
        {
            if(cloneInfoText != null)
            {
                Destroy(cloneInfoText.gameObject);
            }

            cloneInfoText = Instantiate(infoText);
            cloneInfoText.transform.parent = perantTranform;
            cloneInfoText.GetComponent <RectTransform>().localPosition = new Vector2(0.5f, 126.4f);
            cloneInfoText.InfoTextSet(str);

        }


        public void ServerInfomation(string str)
        {
            if (serverinfoText == null)
            {
                return;
            }

            serverinfoText.text = str;
        }

    }
}