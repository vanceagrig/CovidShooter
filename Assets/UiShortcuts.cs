using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiShortcuts : MonoBehaviour
{
    public KeyCode skillsMenuKey;
    public KeyCode pauseKey;
    public KeyCode infoWindowKey;
    public GameObject skillsMenu;
    public GameObject pauseWindow;
    public GameObject infoWindow;
    public GameObject bigMap;
    public GameObject skilBtnInfoWindow;
    public Text qualLevelTextBox;
    [SerializeField] private int qualityLevel;
    public SaveLoadScript saveLoadScript;
    [Space]
    public Slider volumeSlider;
    [Range(0, 1)] public float volume;
    private PlayerStats playerStats;
    private AudioSource[] allAudioSources;
    // Start is called before the first frame update
    void Start()
    {
        qualityLevel = QualitySettings.GetQualityLevel();
        Time.timeScale = 1;
        playerStats = FindObjectOfType<PlayerStats>();
        allAudioSources = playerStats.GetComponentsInChildren<AudioSource>();
        saveLoadScript = FindObjectOfType<SaveLoadScript>();
        qualLevelTextBox.text = "Current Quality Level: " + QualitySettings.GetQualityLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseWindow.activeSelf)
        {
            for(int i=0;i<allAudioSources.Length;i++)
            {
                allAudioSources[i].volume = volumeSlider.value;
            }
        }
        if (Input.GetKeyDown(infoWindowKey))
        {
            if (infoWindow.activeSelf)
            {
                infoWindow.SetActive(false);
            }
            else if (!infoWindow.activeSelf)
            {
                infoWindow.SetActive(true);
            }
        }
        if (Input.GetKeyDown(skillsMenuKey))
        {
            if (skillsMenu.activeSelf)
            {
                skillsMenu.SetActive(false);
                skilBtnInfoWindow.SetActive(false);
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                GameSystem.Instance.StartTimer();
                GameObject.FindObjectOfType<Controller>().LockControl = false;
            }
            else if (!skillsMenu.activeSelf && !pauseWindow.activeSelf && !bigMap.activeSelf)
            {
                GameSystem.Instance.StopTimer();
                skillsMenu.SetActive(true);
                Time.timeScale = 0;
                Cursor.visible = true;
                GameObject.FindObjectOfType<Controller>().LockControl = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(skillsMenu.activeSelf)
            {
                skillsMenu.SetActive(false);
                Time.timeScale = 1;
                Cursor.visible = false;
                GameSystem.Instance.StartTimer();
                GameObject.FindObjectOfType<Controller>().LockControl = false;
                Cursor.lockState = CursorLockMode.Locked;
                skilBtnInfoWindow.SetActive(false);
            }
            else if (!skillsMenu.activeSelf && !pauseWindow.activeSelf)
            {
                Time.timeScale = 0;
                pauseWindow.SetActive(true);
                GameSystem.Instance.StopTimer();
                GameObject.FindObjectOfType<Controller>().LockControl = true;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else if (!skillsMenu.activeSelf && pauseWindow.activeSelf)
            {
                Time.timeScale = 1;
                GameSystem.Instance.StartTimer();
                pauseWindow.SetActive(false);
                GameObject.FindObjectOfType<Controller>().LockControl = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
    public void ReturnToGame()
    {
        Time.timeScale = 1;
        pauseWindow.SetActive(false);
        GameSystem.Instance.StartTimer();
        GameObject.FindObjectOfType<Controller>().LockControl = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void DecreaseQuality()
    {
        qualityLevel = QualitySettings.GetQualityLevel();
        switch (QualitySettings.GetQualityLevel())
        {
            case 0:
                {
                    break;
                }
            case 1:
                {
                    QualitySettings.SetQualityLevel(0, true);
                    qualLevelTextBox.text = "Current Quality Level: " + QualitySettings.GetQualityLevel();
                    break;
                }
            case 2:
                {
                    QualitySettings.SetQualityLevel(1, true);
                    qualLevelTextBox.text = "Current Quality Level: " + QualitySettings.GetQualityLevel();
                    break;
                }
            case 3:
                {
                    QualitySettings.SetQualityLevel(2, true);
                    qualLevelTextBox.text = "Current Quality Level: " + QualitySettings.GetQualityLevel();
                    break;
                }
            case 4:
                {
                    QualitySettings.SetQualityLevel(3, true);
                    qualLevelTextBox.text = "Current Quality Level: " + QualitySettings.GetQualityLevel();
                    break;
                }
        }
    }
    public void IncreseQuality()
    {
        qualityLevel = QualitySettings.GetQualityLevel();
        switch (QualitySettings.GetQualityLevel())
        {
            case 0:
                {
                    QualitySettings.SetQualityLevel(1, true);
                    qualLevelTextBox.text = "Current Quality Level: " + QualitySettings.GetQualityLevel();
                    break;
                }
            case 1:
                {
                    QualitySettings.SetQualityLevel(2, true);
                    qualLevelTextBox.text = "Current Quality Level: " + QualitySettings.GetQualityLevel();
                    break;
                }
            case 2:
                {
                    QualitySettings.SetQualityLevel(3, true);
                    qualLevelTextBox.text = "Current Quality Level: " + QualitySettings.GetQualityLevel();
                    break;
                }
            case 3:
                {
                    QualitySettings.SetQualityLevel(4, true);
                    qualLevelTextBox.text = "Current Quality Level: " + QualitySettings.GetQualityLevel();
                    break;
                }
            case 4:
                {
                    break;
                }
        }
    }
    public void SaveGame()
    {
        saveLoadScript.SaveGame();
    }
    public void LoadLastSaveGame()
    {
        saveLoadScript.LoadGame();
    }
}
