
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

namespace Changho.Managers
{
    public class GamePhotonManager<T> : MonoBehaviourPunCallbacks where T : MonoBehaviourPunCallbacks
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


        virtual public void Awake()
        {

            PhotonNetwork.AutomaticallySyncScene = true;
        }

        protected void TransitionScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);


        }
    }
}
