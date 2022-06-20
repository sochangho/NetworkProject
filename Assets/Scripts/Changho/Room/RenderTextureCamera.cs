using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureCamera : MonoBehaviour
{

    [SerializeField]
    private Transform spwanTransform;

    private GameObject characterObject;

    public void CharacterChange(GameObject selectCharacter)
    {
        if(characterObject != null)
        {
            Destroy(characterObject);
            characterObject = null;
        }

        characterObject = selectCharacter;

    }




}
