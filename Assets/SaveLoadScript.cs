using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveLoadScript : MonoBehaviour
{
    public Transform playerSpawnPosition;
    public bool startLoad;
    [SerializeField]private PlayerStats playerStats;
    [SerializeField]private LoadingScreen loadingScreen;
    [SerializeField]private Slider loadingSlider;
    [SerializeField]private WaveSpawner waveSpawner;
    [SerializeField]private Vector3 playerSavePos;
    [SerializeField]private bool gunPickedUp, riflePickedUp, grenadePickedUp;
    [SerializeField]private int currentMaxLifepoints,currentMaxShieldPoints,totalSkillPointsSpent,availableSkills, currentWaveNumber,playerGunAmmo, playerGrenadeAmmo;
    [SerializeField]private List<Target> enemies;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SaveSystem");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        
    }
    private void Start()
    {
        loadingScreen = FindObjectOfType<LoadingScreen>();
        loadingSlider = loadingScreen.gameObject.GetComponentInChildren<Slider>();
    }

    public void SaveGame()
    {
        FindStatsAndSpawner();
        currentMaxLifepoints = playerStats.playerLifePoints;
        currentMaxShieldPoints = playerStats.playerSieldPoints;
        gunPickedUp = playerStats.GetComponent<Controller>().gunPickedUp;
        riflePickedUp = playerStats.GetComponent<Controller>().riflePickedUp;
        grenadePickedUp = playerStats.GetComponent<Controller>().grenadePickedUp;
        playerGunAmmo = FindObjectOfType<Controller>().GetAmmo(0);
        playerGrenadeAmmo = FindObjectOfType<Controller>().GetAmmo(2);
        totalSkillPointsSpent = playerStats.GetTotalSpentPoints();
        availableSkills = playerStats.availableSkillPoints;
        currentWaveNumber = waveSpawner.waveNumber;
        playerSavePos = playerStats.transform.position;
        if (enemies!=null) { enemies.Clear(); }
        Target[] foundEnemies = FindObjectsOfType<Target>();
        for (int i = 0; i < foundEnemies.Length; i++)
        {
            enemies.Add(foundEnemies[i]);
        }
        SaveToDisk();
    }
    public void LoadGame()
    {
        FindStatsAndSpawner();
        playerStats.currentLifePoints = currentMaxLifepoints;
        playerStats.playerLifePoints = currentMaxLifepoints;
        playerStats.currentSieldPoints = currentMaxShieldPoints;
        playerStats.playerSieldPoints = currentMaxShieldPoints;
        playerStats.ammoPowPointsPistol = 0;
        playerStats.accuracyPointsPistol = 0;
        playerStats.ammoPowPointsRifle = 0;
        playerStats.accuracyPointsRifle = 0;
        playerStats.endurancePoints = 0;
        playerStats.athletePoints = 0;
        playerStats.ammoPowPointsGrenade = 0;
        playerStats.forcePointsGrenade = 0;
        playerStats.GetComponent<Controller>().gunPickedUp = gunPickedUp;
        playerStats.GetComponent<Controller>().riflePickedUp = riflePickedUp;
        playerStats.GetComponent<Controller>().grenadePickedUp = grenadePickedUp;
        GameObject.FindWithTag("Player").transform.position = playerSpawnPosition.position;
        int gunAmmo = FindObjectOfType<Controller>().GetAmmo(0);
        if (gunAmmo < playerGunAmmo)
        {
            FindObjectOfType<Controller>().ChangeAmmo(0,-(Mathf.FloorToInt(FindObjectOfType<Controller>().GetAmmo(0)-playerGunAmmo)));
        }
        int grenadeAmmo = FindObjectOfType<Controller>().GetAmmo(2);
        if (grenadeAmmo < playerGrenadeAmmo)
        {
            FindObjectOfType<Controller>().ChangeAmmo(2,-(Mathf.FloorToInt(FindObjectOfType<Controller>().GetAmmo(2) - playerGrenadeAmmo)));
        }
        playerStats.availableSkillPoints = totalSkillPointsSpent;
        if (currentWaveNumber>1)
        {
            waveSpawner.waveNumber = currentWaveNumber - 1;
        }
        else if(currentWaveNumber <= 1)
        {
            waveSpawner.waveNumber = 1;
        }
        
        waveSpawner.LoadCurrentWave();
        FindObjectOfType<UiShortcuts>().ReturnToGame();
        FindObjectOfType<GameOverMenu>().SetNumberOfLives(3);

        loadingScreen = FindObjectOfType<LoadingScreen>();
        loadingSlider = loadingScreen.gameObject.GetComponentInChildren<Slider>();
        loadingScreen.GetComponent<CanvasGroup>().alpha = 0;

    }
    public void LoadLastSaveMainMenu()
    {
        startLoad = true;
        StartCoroutine(LoadLastSave());
    }
    private void SaveToDisk()
    {
        SaveLoadScript diskSave = FindObjectOfType<GameSystem>().StartPrefabs[3].GetComponent<SaveLoadScript>();
        diskSave.currentWaveNumber = currentWaveNumber;
        diskSave.currentMaxLifepoints = currentMaxLifepoints;
        diskSave.currentMaxShieldPoints = currentMaxShieldPoints;
        diskSave.totalSkillPointsSpent = totalSkillPointsSpent;
        diskSave.availableSkills = availableSkills;
        diskSave.grenadePickedUp = grenadePickedUp;
        diskSave.riflePickedUp = riflePickedUp;
        diskSave.gunPickedUp = gunPickedUp;
        diskSave.playerGrenadeAmmo = playerGrenadeAmmo;
        diskSave.playerGunAmmo = playerGunAmmo;
        diskSave.playerSavePos = playerSavePos;
    }
    private void LoadInfoFromDisk()
    {
        SaveLoadScript diskSave = FindObjectOfType<GameSystem>().StartPrefabs[3].GetComponent<SaveLoadScript>();
        totalSkillPointsSpent = diskSave.totalSkillPointsSpent;
        availableSkills = diskSave.availableSkills;
        currentWaveNumber = diskSave.currentWaveNumber;
        currentMaxLifepoints = diskSave.currentMaxLifepoints;
        currentMaxShieldPoints = diskSave.currentMaxShieldPoints;
        grenadePickedUp = diskSave.grenadePickedUp;
        riflePickedUp = diskSave.riflePickedUp;
        gunPickedUp = diskSave.gunPickedUp;
        playerGrenadeAmmo = diskSave.playerGrenadeAmmo;
        playerGunAmmo = diskSave.playerGunAmmo;
        playerSavePos = diskSave.playerSavePos;
    }
    private IEnumerator LoadLastSave()
    {
        loadingScreen.GetComponent<CanvasGroup>().alpha = 1;
        AsyncOperation op = SceneManager.LoadSceneAsync(1);

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / .01f);
            Debug.Log(op.progress);
            loadingSlider.value = progress;
            yield return null;
        }
        LoadInfoFromDisk();
        yield return new WaitForSeconds(1);
        LoadGame();
    }
    private void FindStatsAndSpawner()
    {
        if (FindObjectOfType<PlayerStats>() != null)
        {
            playerStats = FindObjectOfType<PlayerStats>();
        }
        if (FindObjectOfType<WaveSpawner>() != null)
        {
            waveSpawner = FindObjectOfType<WaveSpawner>();
        }
        if (GameObject.FindWithTag("StartSpawnLocation")!=null)
        {
            playerSpawnPosition = GameObject.FindWithTag("StartSpawnLocation").GetComponent<Transform>();
        }
    }
}
