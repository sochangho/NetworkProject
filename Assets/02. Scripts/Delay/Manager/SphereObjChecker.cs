using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereObjChecker : ObjChecker
{
    PlayerController playerController;
    
    public int number;
     
     

    public SphereObjChecker(PlayerController playerController)
    {
        this.playerController = playerController;
        number = playerController.number;
    }


    public override bool IsPerceive()
    {
        

        Collider[] colliders = Physics.OverlapSphere(playerController.dotPos.position, playerController.detectSize);

        
        for (int i = 0; i < colliders.Length; i++)
        {
            //colliders[i].gameObject.name
            if(colliders[i].tag == "Player")
            {
               
                if(playerController.number == colliders[i].gameObject.GetComponent<PlayerController>().number)
                {
                    continue;
                }

                //Debug.Log("아무거나" + colliders[i].gameObject.name + number + playerController.number +colliders[i].gameObject.GetComponent<PlayerController>().number);

                if(colliders.Length > 0)
                {
                    return true;
                }
                


                
            }

            

            
        }
        
        return false;
    }


}
