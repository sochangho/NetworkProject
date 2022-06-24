using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayObjChecker : ObjChecker
{
    PlayerController playerController;

    public RayObjChecker(PlayerController playerController)
    {
        this.playerController = playerController;
    }
    

    public override bool IsPerceive()
    {
        Ray ray = new Ray(playerController.transform.position, playerController.transform.forward);

        Debug.DrawRay(playerController.transform.position, playerController.transform.forward,Color.red);

        if(Physics.Raycast(ray, playerController.detectSize))
        {
            
            return true;
        }
        
        
        return false;
    }
}
