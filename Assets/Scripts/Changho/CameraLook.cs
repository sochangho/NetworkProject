using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Changho
{
    public class CameraLook : MonoBehaviour
    {
        Transform cam;
        


        // Start is called before the first frame update
        void Start()
        {
            cam = Camera.main.transform;
        }


        void Update()
        {
            transform.LookAt(transform.position + cam.rotation *
                Vector3.forward, cam.rotation * Vector3.up);
        }
    }
}
