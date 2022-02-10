using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public GameObject ammoPrefab;
    [Space]
    public Transform[] spawnLocations;
    public int enemySpawnNumber;
    public int bossLevelInterval;
    public float spawnWaveDelay;
    private float countdownDelay;
    public int waveNumber;
    public int enganceEnemyEveryNLevels;
    public List<GameObject> spawnedEnemies;
    private bool spawned, countDownStarted, enemiesEnhanced;
    private Vector3 spawnPos;
    public EnemySpawner SpecialEventSpawner;
    public int specialEventInterval;
    private bool specialEventStarted;
    private bool specialEventFinished, waitForNextWave;
    private int thisWaveNumber;
    private GameOverMenu gameOverMenu;
    private bool isGameOver,checkedEnemyStatus, ammoSpawned;
    private SaveLoadScript saveLoadScript;
    // Start is called before the first frame update
    private void Start()
    {
        spawnPos = spawnLocations[Mathf.FloorToInt(Random.Range(0,spawnLocations.Length))].position;
        countdownDelay = spawnWaveDelay;
        gameOverMenu = FindObjectOfType<GameOverMenu>();
        saveLoadScript = FindObjectOfType<SaveLoadScript>();
        SpawnWave();
    }
    private void Update()
    {
        isGameOver = gameOverMenu.isGameOver;
        if (spawned && !isGameOver)
        {
            GameObject.FindObjectOfType<LevelEnemyText>().ClearCountDown();
            if (spawnedEnemies.Count > 0)
            {
                GameObject.FindObjectOfType<LevelEnemyText>().UpdateLevelEnemyTexts();
            }
            if (!checkedEnemyStatus)
            {
                checkedEnemyStatus = true;
                StartCoroutine(CheckEnemyStatus());
            }
            if (spawnedEnemies.Count == 0 && !countDownStarted)
            {
                countDownStarted = true;
                spawned = false;
                StartCoroutine(PrepareForNextWave());
            }
        }
        
        if (spawnedEnemies.Count == 0)
        {
            GameObject.FindObjectOfType<LevelEnemyText>().SetEnemiesTo0();
        }
        if (countDownStarted)
        {
            countdownDelay -= Time.deltaTime;
            GameObject.FindObjectOfType<LevelEnemyText>().UpdateCountdownTXT(countdownDelay);
        }
       
        if (waveNumber%specialEventInterval==0)
        {
            if(!specialEventStarted && !specialEventFinished && !waitForNextWave)
            {
                specialEventStarted = true;
                SpawnSpecialEvent();
            }
        }
        if (specialEventStarted)
        {
            GameObject.FindObjectOfType<SpecialEventText>().UpdateLevelEnemyText(SpecialEventSpawner.spawnedEnemies.Count.ToString());

            if (SpecialEventSpawner.spawnedEnemies.Count == 0)
            {
                specialEventFinished = true;
                GameObject.FindObjectOfType<SpecialEventText>().ClearSpecialEventText();
            }
        }
        if (specialEventFinished==true)
        {
            if (!waitForNextWave)
            {
                waitForNextWave = true;
                thisWaveNumber = waveNumber;
            }
            if (waitForNextWave)
            {
                if(thisWaveNumber<waveNumber)
                {
                    specialEventStarted = false;
                    specialEventFinished = false;
                    waitForNextWave = false;
                }
            }
        }
        if (Controller.Instance.GetAmmo(0)==0 && !ammoSpawned)
        {
            ammoSpawned = true;
            int j = 3;
            for (int i = 0; i < j; i++)
            {
                GameObject obj = Instantiate(ammoPrefab, spawnLocations[i].position, Quaternion.identity);
            }
            StartCoroutine(ResetAmmoSpawn());
        }
    }
    private IEnumerator ResetAmmoSpawn()
    {
        yield return new WaitForSeconds(60);
        ammoSpawned = false;
    }
    private IEnumerator CheckEnemyStatus()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (spawnedEnemies[i] == null)
            {
                spawnedEnemies.Remove(spawnedEnemies[i]);
            }
        }
        checkedEnemyStatus = false;
    }
    private IEnumerator PrepareForNextWave()
    {

        yield return new WaitForSeconds(spawnWaveDelay);
        countDownStarted = false;
        SpawnWave();
        countdownDelay = spawnWaveDelay;
    }
    private void SpawnEnemies()
    {
        if (waveNumber<=5)
        {
            spawnPos = spawnLocations[Mathf.FloorToInt(Random.Range(0, 3))].position;
        }
        if(waveNumber>5)
        {
            spawnPos = spawnLocations[Mathf.FloorToInt(Random.Range(0, spawnLocations.Length))].position;
        }
        GameObject obj = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        spawnedEnemies.Add(obj);
        if (waveNumber % enganceEnemyEveryNLevels == 0)
        {
            EnhanceEnemies(obj, waveNumber*10, Mathf.FloorToInt(waveNumber / 2));
        }
    }
    private void SpawnBoss()
    {
      SpawnMultipleBoss(waveNumber/2);
    }
    private void SpawnWave()
    {
        if (!GameObject.FindWithTag("SaveSystem").GetComponent<SaveLoadScript>().startLoad)
        {
            if (!spawned)
            {
                spawned = true;
                waveNumber += 1;
                enemySpawnNumber = 10 + waveNumber;
                if (waveNumber % bossLevelInterval == 0)
                {
                    Debug.Log("Boss Level");
                    SpawnBoss();
                    for (int i = 0; i < enemySpawnNumber; i++)
                    {
                        SpawnEnemies();
                    }
                }
                else if (enemySpawnNumber > 0)
                {
                    for (int i = 0; i < enemySpawnNumber; i++)
                    {
                        SpawnEnemies();
                    }
                }
            }
            if (FindObjectOfType<SaveLoadScript>() != null)
            {
                bool startLoad = FindObjectOfType<SaveLoadScript>().startLoad;
                if (startLoad)
                {
                    return;
                }
                else if (!startLoad)
                {
                    saveLoadScript.SaveGame();
                }
            }
        }
        else if (GameObject.FindWithTag("SaveSystem").GetComponent<SaveLoadScript>().startLoad)
        {
            GameObject.FindWithTag("SaveSystem").GetComponent<SaveLoadScript>().startLoad = false;
        }
    }
    private void SpawnSpecialEvent()
    {
        int i = waveNumber / specialEventInterval;
        SpecialEventSpawner.spawnNumber = i;
        SpecialEventSpawner.SpawnEnemies();
    }
    private void SpawnMultipleBoss(int num)
    {
        for (int i = 0; i < num; i++)
        {
            spawnPos = new Vector3(spawnPos.x + Random.Range(1, 5), spawnPos.y, spawnPos.z + Random.Range(1, 5));
            GameObject obj = Instantiate(bossPrefab, spawnPos, Quaternion.identity);
            switch(waveNumber)
            {
                case 4:
                    {
                        obj.GetComponent<Enemy>().closeDMGPerSec += 4;
                        obj.GetComponent<Target>().health += 300;
                        obj.GetComponent<Enemy>().enemySpeed += 0.5f;
                        obj.GetComponent<Enemy>().fireInterval -= 0.5f;
                        break;
                    }
                case 6:
                    {
                        obj.GetComponent<Enemy>().closeDMGPerSec += 5;
                        obj.GetComponent<Target>().health += 600;
                        obj.GetComponent<Enemy>().enemySpeed += 0.6f;
                        obj.GetComponent<Enemy>().fireInterval -= 0.6f;
                        break;
                    }
                case 8:
                    {
                        obj.GetComponent<Enemy>().closeDMGPerSec += 8;
                        obj.GetComponent<Target>().health += 900;
                        obj.GetComponent<Enemy>().enemySpeed += 0.8f;
                        obj.GetComponent<Enemy>().fireInterval -= 0.8f;
                        break;
                    }
                case 10:
                    {
                        obj.GetComponent<Enemy>().closeDMGPerSec += 12;
                        obj.GetComponent<Target>().health += 1200;
                        obj.GetComponent<Enemy>().enemySpeed += 0.8f;
                        obj.GetComponent<Enemy>().fireInterval -= 0.8f;
                        break;
                    }
                case 12:
                    {
                        obj.GetComponent<Enemy>().closeDMGPerSec += 15;
                        obj.GetComponent<Target>().health += 1500;
                        obj.GetComponent<Enemy>().enemySpeed += 1f;
                        obj.GetComponent<Enemy>().fireInterval -= 1f;
                        break;
                    }
                case 14:
                    {
                        obj.GetComponent<Enemy>().closeDMGPerSec += 18;
                        obj.GetComponent<Target>().health += 1800;
                        obj.GetComponent<Enemy>().enemySpeed += 1.2f;
                        obj.GetComponent<Enemy>().fireInterval -= 1.2f;
                        break;
                    }
                case 16:
                    {
                        obj.GetComponent<Enemy>().closeDMGPerSec += 21;
                        obj.GetComponent<Target>().health += 2100;
                        obj.GetComponent<Enemy>().enemySpeed += 1.4f;
                        obj.GetComponent<Enemy>().fireInterval -= 1.4f;
                        break;
                    }
            }
            spawnedEnemies.Add(obj);
            spawnPos = spawnLocations[Mathf.FloorToInt(Random.Range(0, spawnLocations.Length))].position;
        }
    }
    private void EnhanceEnemies(GameObject enemy,int life,int scorePoints)
    {
        enemiesEnhanced = false;
        if (!enemiesEnhanced)
        {
            enemiesEnhanced = true;
            enemy.GetComponent<Target>().health += life;
            enemy.GetComponent<Target>().pointValue += scorePoints;
        }
    }
    public void LoadCurrentWave()
    {
        if (spawnedEnemies !=null)
        {
            for (int i = 0; i < spawnedEnemies.Count; i++)
            {
                Destroy(spawnedEnemies[i]);
            }
            spawnedEnemies.Clear();
        }
        
        if (!spawned)
        {
            spawned = true;
            if (waveNumber % bossLevelInterval == 0)
            {
                SpawnBoss();
                for (int i = 0; i < enemySpawnNumber; i++)
                {
                    SpawnEnemies();
                }
            }
            else if (enemySpawnNumber > 0)
            {
                for (int i = 0; i < enemySpawnNumber; i++)
                {
                    SpawnEnemies();
                }
            }
        }
        
    }
}
