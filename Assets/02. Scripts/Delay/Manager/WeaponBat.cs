using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Jiyeon
{
    public class WeaponBat : MonoBehaviour
    {
        public float rate;
        //public BoxCollider meleeArea;
        // public TrailRenderer trailEffect;

        public void Use()
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }

        private IEnumerator Swing()
        {

            //meleeArea.enabled = true;
            //trailEffect.enabled = true;
            

            yield return new WaitForSeconds(0.3f);
            //meleeArea.enabled = false;
            //trailEffect.enabled = false;

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                //킬카운트 증가
               // meleeArea.enabled = false;
            }
        }

    }
}
