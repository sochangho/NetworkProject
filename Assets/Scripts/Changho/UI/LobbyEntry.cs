﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Changho.UI
{
    public class LobbyEntry : MonoBehaviour
    {
        [SerializeField]
        private Text roomNameText;
        [SerializeField]
        private Text playersText;
        [SerializeField]
        private Button button; 

        public void EntrySetting(string roomname , string players , 
            UnityEngine.Events.UnityAction action)
        {

            roomNameText.text = roomname;
            playersText.text = players;
            button.onClick.AddListener(action);


        }



    }
}