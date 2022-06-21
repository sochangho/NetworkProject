using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Changho.UI;
using Changho.Managers;

namespace Changho.Room
{
    public class CharactersCamRender : GameManager<CharactersCamRender>  
    {
        [System.Serializable]
        public class RenderTextureValue
        {

            public RenderTexture renderTexture;
            public Camera camera { get; set; } = null;
            private bool use = false;
            public bool Use { get { return use; }  
                set {

                    use = value;
                    if (!use)
                    {
                        Destroy(camera.gameObject);
                        camera = null;
                    }
                    else
                    {
                        camera.targetTexture = renderTexture;
                    }
               
                
                } 
            
            }
            
        }


        [SerializeField]
        private Camera renderCam;

        [SerializeField]
        private RenderTextureValue[] renderTextures;

        public PlayerOwnEntry playerOwnEntry;

        public void CharaterInit(Dictionary<int , PlayerEntry> entryFairs)
        {
            renderTextures[0].camera = playerOwnEntry.ownPlayerCamera; 
            playerOwnEntry.CharecterSet(renderTextures[0]);


            int i = 1;
            foreach(var entryFair in entryFairs)
            {
                Camera c = Instantiate(renderCam);
                c.transform.parent = this.transform;
                c.transform.localPosition = new Vector3(i * 40, 0, 0);
                renderTextures[i].camera = c;
                renderTextures[i].Use = true;
                entryFair.Value.CharecterSet(renderTextures[i]);
                CharacterChange(entryFair.Value);
                i++;
            }



        }


        public void CharacterAdd(PlayerEntry entry)
        {
            for(int i = 1; i < renderTextures.Length; i++)
            {
                if (!renderTextures[i].Use)
                {
                    Camera c = Instantiate(renderCam);
                    c.transform.parent = this.transform;
                    c.transform.localPosition = new Vector3(i * 40, 0, 0);
                    renderTextures[i].camera = c;
                    renderTextures[i].Use = true;
                    entry.CharecterSet(renderTextures[i]);
                    CharacterChange(entry);
                    break;
                }

            }


        }

        public void CharacterDelete(PlayerEntry entry)
        {
            entry.DeleteCharacter();

        }

        public void CharacterChange(PlayerEntry entry)
        {
            
           var renderTextureValue = entry.GetRenderTextureValue();

           CharacterType characterType =  entry.characterType;
           string path = string.Format("Changho/Prefaps/Characters/{0}", characterType.ToString());
         
           GameObject go= Resources.Load<GameObject>(path);

            Debug.Log(go);
           renderTextureValue.camera.GetComponent<RenderTextureCamera>().CharacterChange(go);

        }



    }



   
}