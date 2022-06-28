using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameEndPlayerRankingUI : MonoBehaviour
{
    public Text text;
    public GameObject Medal;
    public void textUpdate(string playerName) {
        text.text = playerName.ToString();
    }
}
