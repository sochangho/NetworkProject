using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Changho.Managers
{
    public class PlayerHitManager : GamePhotonManager<PlayerHitManager>
    {






        #region PhotonCallBacks
        public virtual void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {

        }
        #endregion


        public void PlayerDie(int number , Player player)
        {


            var playerProps = player.CustomProperties;
            ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
            if (!player.CustomProperties.ContainsKey(ConfigData.HITPLAYER))
            {
                
                foreach(var prop in playerProps)
                {
                    properties.Add(prop.Key , prop.Value);

                }

                properties.Add(ConfigData.HITPLAYER, number);

                player.SetCustomProperties(properties);

                return;
            }


            foreach (var prop in playerProps)
            {
                properties.Add(prop.Key, prop.Value);

            }

            properties[ConfigData.HITPLAYER] = number;
            player.SetCustomProperties(properties);
        }



    }
}