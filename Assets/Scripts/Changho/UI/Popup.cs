using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Changho.UI
{

    public class Popup : MonoBehaviour
    {

       

        virtual public void OnOpen() { }

        virtual public void OnClose() => Destroy(gameObject);

    }
}
