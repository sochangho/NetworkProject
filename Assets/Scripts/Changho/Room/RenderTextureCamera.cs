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
        GameObject go  = Instantiate(selectCharacter);
       
        go.transform.parent = this.transform;
        go.transform.position = spwanTransform.position;
        go.transform.rotation = spwanTransform.rotation;

        if(characterObject != null)
        {
            Destroy(characterObject);
            characterObject = null;
        }

        characterObject = go;

    }

    public void CharacterReadyAnim(bool value)
    {
       Animator characteranim = characterObject.GetComponent<Animator>();
       characteranim.SetBool("Ready", value);
    }





}
