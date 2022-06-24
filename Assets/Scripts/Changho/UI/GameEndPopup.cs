using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


namespace Changho.UI
{
    public class GameEndPopup : Popup
    {


        public UnityAction onClickExitEventAction;



        public void Start()
        {
            OnOpen();
        }


        public override void OnOpen()
        {
            base.OnOpen();
            Invoke("OnClose", 3.0f);

        }

        public override void OnClose()
        {
            if(onClickExitEventAction != null)
            {
                onClickExitEventAction();
            }

                   
        }

 

    }
}