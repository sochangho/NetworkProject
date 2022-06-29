using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



namespace Changho.UI
{
    public class PlayerGameNameUI : MonoBehaviour
    {   
        

        [SerializeField]
        private TextMeshProUGUI playerNameText;

        [SerializeField]
        private Image backImage;


        public void SetPlayerNameText(string playerName , Sprite sprite )
        {
            backImage.sprite = sprite;
            playerNameText.text = playerName;
        }

         
    }


    
}
