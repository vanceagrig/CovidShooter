using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingSlider;
    private void Start()
    {
        loadingScreen = FindObjectOfType<LoadingScreen>().gameObject;
    }
    public void LoadLevel()
    {
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<CanvasGroup>().alpha = 1;
        StartCoroutine(LoadSceneAsync());
    }

    public IEnumerator LoadSceneAsync()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(1);

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / .01f);
            Debug.Log(op.progress);
            loadingSlider.value = progress;
            yield return null;
        }
    }
    public void NewGameStart()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
