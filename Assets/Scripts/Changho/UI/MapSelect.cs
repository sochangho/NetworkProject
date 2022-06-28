using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Changho.UI
{
    public class MapSelect : MonoBehaviour
    {
        
        public Dropdown dropdown;


        public Image image;

        public UnityAction<int> action;

        public void Start()
        {

            dropdown.onValueChanged.AddListener(action);




        }


       


        public void SetDropDownMap(List<Room.Room.MapScene> datas , bool active , int value)
        {

            

            List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();

            for(int i = 0; i < datas.Count; i++)
            {
               Dropdown.OptionData optionData = new Dropdown.OptionData();

               Room.Room.MapScene mapScene = (Room.Room.MapScene)datas[i];

                optionData.text = mapScene.scenename;
                optionData.image = datas[i].image; 
                optionDatas.Add(optionData);

            }


            dropdown.AddOptions(optionDatas);
            dropdown.value = value;

            image.sprite = datas[value].image;

            dropdown.gameObject.SetActive(active);

        }


     

    }
}