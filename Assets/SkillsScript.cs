using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsScript : MonoBehaviour
{
    private PlayerStats playerStats;
    public Button enduranceButton;
    public Button ammoPistolPowButton;
    public Button accuracyPistolButton;
    public Button ammoRiflePowButton;
    public Button accuracyRifleButton;
    public Button ammoGrenadePowButton;
    public Button forceGrenadeButton;
    public Button athleteButton;
    public Text enduranceSkillPointsTXT;
    public Text ammoPowPistolSkillPointsTXT;
    public Text accuracyPistolSkillPointsTXT;
    public Text ammoPowRifleSkillPointsTXT;
    public Text accuracyRifleSkillPointsTXT;
    public Text ammoPowGrenadeSkillPointsTXT;
    public Text throwForceGrenadeSkillPointsTXT;
    public Text athleteSkillPointsTXT;
    public Slider toAthleteSlider;
    public Text availableSkillPointsTxt;
    public Text pistolInfoTxt;
    public Text rifleInfoTxt;
    public Text grenadeInfoTxt;
    public Text genStatsInfoTxt;
    private int availableSkillPoints;
    private bool _0SkillPoints, _availSkillPoints;
    // Start is called before the first frame update
    void Awake()
    {
        playerStats = GameObject.FindObjectOfType<PlayerStats>();
        enduranceButton.interactable = false;
        athleteButton.interactable = false;

        ammoPistolPowButton.interactable = false;
        accuracyPistolButton.interactable = false;

        ammoRiflePowButton.interactable = false;
        accuracyRifleButton.interactable = false;

        ammoGrenadePowButton.interactable = false;
        forceGrenadeButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        availableSkillPoints = playerStats.availableSkillPoints;
        if (this.gameObject.activeSelf)
        {
            UpdateGenStatsInfoTxt();
            UpdateWeaponInfoTxt();
            if (availableSkillPoints == 0)
            {
                if (!_0SkillPoints)
                {
                    _0SkillPoints = true;
                    enduranceButton.interactable = false;

                    ammoPistolPowButton.interactable = false;
                    accuracyPistolButton.interactable = false;

                    ammoRiflePowButton.interactable = false;
                    accuracyRifleButton.interactable = false;

                    ammoGrenadePowButton.interactable = false;
                    forceGrenadeButton.interactable = false;
                    
                    availableSkillPointsTxt.text = "0";
                }
            }
            else if (availableSkillPoints > 0)
            {
                _0SkillPoints = false;
                enduranceButton.interactable = true;
                if (playerStats.GetComponent<Controller>().gunPickedUp)
                {
                    ammoPistolPowButton.interactable = true;
                    accuracyPistolButton.interactable = true;
                    
                }
                if (playerStats.GetComponent<Controller>().riflePickedUp)
                {
                    ammoRiflePowButton.interactable = true;
                    accuracyRifleButton.interactable = true;
                }
                if (playerStats.GetComponent<Controller>().grenadePickedUp)
                {
                    ammoGrenadePowButton.interactable = true;
                    forceGrenadeButton.interactable = true;
                }
                if (GameObject.FindObjectOfType<PlayerStats>().endurancePoints > 2)
                {
                    athleteButton.interactable = true;
                }
                else if (GameObject.FindObjectOfType<PlayerStats>().endurancePoints < 2)
                {
                    athleteButton.interactable = false;
                }
                availableSkillPointsTxt.text = availableSkillPoints.ToString();
            }
            if (playerStats.endurancePoints <= 4)
            {
                toAthleteSlider.value = playerStats.endurancePoints;
                athleteButton.interactable = false;
            }
            else if (availableSkillPoints > 0 && playerStats.endurancePoints>4)
            {
                athleteButton.interactable = true;
            }
        }
       
    }
    public void AddEndurancePoint()
    {
        playerStats.AddEndurancePoint();
        enduranceSkillPointsTXT.text = playerStats.endurancePoints.ToString();
    }
    public void AddAmmoPowPointPistol()
    {
        playerStats.AddAmmoPowPointPistol();
        ammoPowPistolSkillPointsTXT.text = playerStats.ammoPowPointsPistol.ToString();
    }
    public void AddAccuracyPointPistol()
    {
        playerStats.AddAccuracyPointPistol();
        accuracyPistolSkillPointsTXT.text = playerStats.accuracyPointsPistol.ToString();
    }
    public void AddAmmoPowPointRifle()
    {
        playerStats.AddAmmoPowPointRifle();
        ammoPowRifleSkillPointsTXT.text = playerStats.ammoPowPointsRifle.ToString();
    }
    public void AddAccuracyPointRifle()
    {
        playerStats.AddAccuracyPointRifle();
        accuracyRifleSkillPointsTXT.text = playerStats.accuracyPointsRifle.ToString();
    }
    public void AddAmmoPowPointGrenade()
    {
        playerStats.AddAmmoPowPointGrenade();
        ammoPowGrenadeSkillPointsTXT.text = playerStats.ammoPowPointsGrenade.ToString();
    }
    public void AddThrowForcePointGrenade()
    {
        playerStats.AddForcePointGrenade();
        throwForceGrenadeSkillPointsTXT.text = playerStats.forcePointsGrenade.ToString();
    }
    public void AddAthletePoint()
    {
        playerStats.AddAthletePoint();
        athleteSkillPointsTXT.text = playerStats.athletePoints.ToString();
    }
    public void UpdateWeaponInfoTxt()
    {
        pistolInfoTxt.text = "Pistol DMG = " + playerStats.pistolPrefab.damage.ToString() + "\n" +"Accuracy = " + playerStats.pistolPrefab.advancedSettings.spreadAngle.ToString();
        rifleInfoTxt.text = "Rifle DMG = " + playerStats.riflePrefab.damage.ToString() + "\n" + "Accuracy = " + playerStats.riflePrefab.advancedSettings.spreadAngle.ToString();
        grenadeInfoTxt.text = "Grenade DMG = " + playerStats.grenadePrefab.damage.ToString() + "\n" + "Throw force = " + playerStats.grenadePrefab.projectileLaunchForce.ToString();
    }
    public void UpdateGenStatsInfoTxt()
    {
        genStatsInfoTxt.text = "Normal speed = " + Controller.Instance.PlayerSpeed.ToString() + "\n" + "Run speed = " + Controller.Instance.RunningSpeed.ToString();
    }
    private IEnumerator StartGetInfo()
    {
        yield return new WaitForSeconds(3);
        UpdateWeaponInfoTxt();
        UpdateGenStatsInfoTxt();
    }
}
