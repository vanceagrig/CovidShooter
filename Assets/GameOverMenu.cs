using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    private PlayerStats playerStats;
    private float currentLife;
    private int numberOfLivesLeft;
    public bool isGameOver = false;
    public bool playerIsDead;
    private SaveLoadScript saveLoadScript;
    [SerializeField]private GameObject player;
    [SerializeField]private float origSpeed=5, origRunSpeed=10;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindObjectOfType<PlayerStats>();
        saveLoadScript = FindObjectOfType<SaveLoadScript>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
      
        currentLife = playerStats.currentLifePoints;
        numberOfLivesLeft = playerStats.numberOfLives;
        if (numberOfLivesLeft > 0)
        {
            if (currentLife <= 0 && !playerIsDead)
            {
                playerIsDead = true;
                playerStats.RespawnPlayerIfDead();
                
            }
        }
        else if (numberOfLivesLeft <= 0)
        {
            if (currentLife <= 0)
            {
                isGameOver = true;
            }
            else if (currentLife > 0)
            {
                isGameOver = false;
            }
            CheckForGameOver();
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void CheckForGameOver()
    {
        if (isGameOver)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            gameObject.GetComponent<CanvasGroup>().interactable = true;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else if (!isGameOver)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<CanvasGroup>().interactable = false;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
    
    public void SetNumberOfLives(int i)
    {
        numberOfLivesLeft = i;
    }
    public void AddNumberOfLives(int i)
    {
        numberOfLivesLeft += i;
    }
}
