using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


namespace Changho.UI
{
    public class GameEndPopup : Popup
    {


        [SerializeField]
        private Button button;


        public UnityAction onClickExitEventAction;

   

        public override void OnOpen()
        {
            base.OnOpen();
            button.onClick.AddListener(OnClose);

            Invoke("Close", 3.0f);

        }

        public override void OnClose()
        {
            if(onClickExitEventAction != null)
            {
                onClickExitEventAction();
            }

            button.interactable = false;
            //base.OnClose();
            
        }

        private void Close()
        {


            if(button.interactable == true)
            {

                OnClose();
            }
        }

 

    }
}