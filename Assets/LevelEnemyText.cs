using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnemyText : MonoBehaviour
{
    public Text levelInfoTXT;
    public Text enemiesInfoTXT;
    public Text countdownInfoTXT;
    public void UpdateLevelEnemyTexts()
    {
        levelInfoTXT.text ="Wave : " +GameObject.FindObjectOfType<WaveSpawner>().waveNumber.ToString();
        enemiesInfoTXT.text = "Enemies : "+ GameObject.FindObjectOfType<WaveSpawner>().spawnedEnemies.Count.ToString();
    }
    public void UpdateCountdownTXT(float countDownTime)
    {
        
        countdownInfoTXT.text = countDownTime.ToString("0");
    }
    public void SetEnemiesTo0()
    {
        enemiesInfoTXT.text = "Enemies : 0";
    }
    public void ClearCountDown()
    {
        countdownInfoTXT.text = "";
    }
}
