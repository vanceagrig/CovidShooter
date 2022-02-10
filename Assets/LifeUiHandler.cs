using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUiHandler : MonoBehaviour
{
    public PlayerStats playerStats;
    public Slider lifeSlider;
    public Slider sieldSlider;
    public Text LPText;
    public Text SPText;
    public Text livesText;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindObjectOfType<PlayerStats>();
        lifeSlider.maxValue = playerStats.playerLifePoints;
        lifeSlider.value = playerStats.playerLifePoints;
        sieldSlider.maxValue = playerStats.playerSieldPoints;
        sieldSlider.value = playerStats.playerSieldPoints;
    }

    // Update is called once per frame
    void Update()
    {
        lifeSlider.value = playerStats.currentLifePoints;
        sieldSlider.value = playerStats.currentSieldPoints;
        LPText.text = playerStats.currentLifePoints + " / " + playerStats.playerLifePoints;
        SPText.text = playerStats.currentSieldPoints + " / " + playerStats.playerSieldPoints;
        livesText.text = playerStats.numberOfLives.ToString();
    }
}
