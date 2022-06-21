using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



namespace Changho.UI
{
    public class PlayerOwnEntry : PlayerEntry
    {

        [SerializeField]
        private Button selectButton;

        [SerializeField]
        private CharacterSelectPopup characterSelect;

        [SerializeField]
        private Room.Room room;


        public Camera ownPlayerCamera;

        private void Start()
        {

            selectButton.onClick.AddListener(OnClickCharacterSelect);
        }

        private void OnClickCharacterSelect()
        {
            room.buttonExit.interactable = false;
            room.buttonReady.interactable = false;
            room.buttonGameStart.interactable = false;


           var popup = Instantiate(characterSelect);
           popup.transform.parent = this.transform.parent;
           popup.GetComponent<RectTransform>().localPosition = Vector2.zero;

        }


    }
}
