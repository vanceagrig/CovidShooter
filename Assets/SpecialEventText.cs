using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialEventText : MonoBehaviour
{
    private string enemiesInfo = "Enemies = ";
    public Text enemiesTxt, countDownTxt;

    public void ClearSpecialEventText()
    {
        enemiesTxt.text = "";
        countDownTxt.text = "";
    }
    public void UpdateLevelEnemyText(string _1)
    {
        enemiesTxt.text = enemiesInfo + _1;
    }
    public void UpdateCountdownTXT(float countDownTime)
    {
        countDownTxt.text = countDownTime.ToString("0");
    }
}
