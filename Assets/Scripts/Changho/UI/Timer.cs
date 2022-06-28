using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Changho.UI
{
    public class Timer : MonoBehaviour
    {
        public Text timertext;

        public void BeforeGameTextSet(string countStr)
        {

            timertext.text = countStr;

        }


        public void GameCountDownTextSet(int count, int max)
        {


            int curM = count / 60;
            int curSecond = count % 60;

            int maxM = max / 60;
            int maxSecond = max % 60;
            string curStrM = curM.ToString();
            string curStrSecond = null;
            string maxStrM = maxM.ToString();
            string maxStrSecond = null;
            
            if(curSecond < 10)
            {
                curStrSecond = string.Format("0{0}", curSecond.ToString());

            }
            else
            {
                curStrSecond = curSecond.ToString();

            }


            if(maxSecond < 10)
            {
                maxStrSecond = string.Format("0{0}", maxSecond.ToString());
            }
            else
            {
                maxStrSecond = maxSecond.ToString();
            }
           




            string countStrFormat = string.Format("{0}:{1} / {2}:{3}", curStrM, curStrSecond 
                , maxStrM,maxStrSecond );
            timertext.fontSize = 20;
            timertext.text = countStrFormat;



        }


    }
}