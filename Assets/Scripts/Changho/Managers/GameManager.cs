using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Changho.Managers
{
    public class GameManager<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {

            get
            {

                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {

                    var go = new GameObject(typeof(T).Name);
                    _instance = go.AddComponent<T>();

                }

                return _instance;
            }



        }

    }
}