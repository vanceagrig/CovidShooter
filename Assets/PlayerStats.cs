using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int numberOfLives=3;
    public int playerLifePoints;
    public int currentLifePoints;
    public int playerSieldPoints;
    public int currentSieldPoints;
    [SerializeField]private bool shieldEmpty;
    public int availableSkillPoints;
    private int totalSkillPointsSpent;
    public int endurancePoints;
    [SerializeField]private Light flashLight;
    [SerializeField]private bool flashLightPickedUp=true,takenShieldDamage, takenLifeDamage,takenDmgCooldowwn;
    #region
    [Space]
    public int ammoPowPointsPistol;
    public int accuracyPointsPistol;
    public int ammoPowPointsRifle;
    public int accuracyPointsRifle;
    public int ammoPowPointsGrenade;
    public int forcePointsGrenade;
    public List<Weapon> weaponPrefabs;
    public Weapon riflePrefab;
    public Weapon pistolPrefab;
    public Weapon grenadePrefab;
    #endregion
    [Space]
    public int athletePoints;
    [Space]
    public int sateliteLaserAmmo;
    [Space]
    public GameObject AimCamera;
    [Space]
    public PlayerHitAudio dmgTakenAudioPlayer;
    [Space]
    public bool GodMode;
    [SerializeField] private Vector3 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        currentLifePoints = playerLifePoints;
        currentSieldPoints = playerSieldPoints;
        StartCoroutine(GetWeaponsPrefabs());
        flashLight = GetComponentInChildren<Light>();
        dmgTakenAudioPlayer = GetComponentInChildren<PlayerHitAudio>();
        spawnPos = FindObjectOfType<PlayerSpawnPos>().transform.position;
        
    }
    private void Update()
    {
       
        GodModeOn();
        AimCameraAction();
        if (takenLifeDamage || takenShieldDamage)
        {
            if (!takenDmgCooldowwn)
            {
                takenDmgCooldowwn = true;
                StartCoroutine(TakenDmgCoolDown());
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            ShowHideFlashLight();
        }
    }
    private void AimCameraAction()
    {
        if (Input.GetMouseButton(1))
        {
            AimCamera.GetComponent<Camera>().fieldOfView = 40;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            AimCamera.GetComponent<Camera>().fieldOfView = 70;
        }
    }
    private IEnumerator TakenDmgCoolDown()
    {
        yield return new WaitForSeconds(2f);
        takenShieldDamage = false;
        takenLifeDamage = false;
        takenDmgCooldowwn = false;
    }
    private IEnumerator GetWeaponsPrefabs()
    {
        yield return new WaitForSeconds(0.5f);
        pistolPrefab = weaponPrefabs[0];
        riflePrefab = weaponPrefabs[1];
        grenadePrefab = weaponPrefabs[2];
        
    }
    private void ShowHideFlashLight()
    {
        if (flashLightPickedUp)
        {
            if(flashLight.gameObject.activeSelf)
            {
                flashLight.gameObject.SetActive(false);
            }
            else if (!flashLight.gameObject.activeSelf)
            {
                flashLight.gameObject.SetActive(true);
            }
        }
    }
    public void GiveDMGToPlayer(int dmg)
    {
        int difference =(currentSieldPoints - dmg);
        if (difference>0 && currentSieldPoints>0)
        {
            takenShieldDamage = true;
            FindObjectOfType<TakeDmgRedSCreen>().takenShieldDamage = true;
            currentSieldPoints -= dmg;
        }
        if (difference<0 && currentSieldPoints > 0)
        {
            FindObjectOfType<TakeDmgRedSCreen>().takenLifeDamage = true;
            FindObjectOfType<TakeDmgRedSCreen>().takenShieldDamage = true;
            takenShieldDamage = true;
            takenLifeDamage = true;
            shieldEmpty = true;
            currentSieldPoints = 0;
            currentLifePoints -= -(difference);
            difference = 0;
        }
        if (shieldEmpty && difference < 0)
        {
            FindObjectOfType<TakeDmgRedSCreen>().takenLifeDamage = true;
            takenLifeDamage = true;
            currentLifePoints -= dmg;
        }
        if (dmg>currentLifePoints)
        {
            currentLifePoints = 0;
        }

    }
    public void AddAvailableSkillPoints(int points)
    {
        availableSkillPoints += points;
    }
    
    public void SpendSkillPoint()
    {
        availableSkillPoints -= 1;
        totalSkillPointsSpent += 1;
    }
    public void AddEndurancePoint()
    {
        endurancePoints += 1;
        SpendSkillPoint();
        AddMaxLifePoints(10);
        AddMaxShieldPoints(5);
    }
    public void AddAmmoPowPointPistol()
    {
        ammoPowPointsPistol += 1;
        SpendSkillPoint();
        pistolPrefab.damage += 3;
    }
    public void AddAccuracyPointPistol()
    {
        if (accuracyPointsPistol>=4)
        {
            return;
        }
        else
        {
            accuracyPointsPistol += 1;
            SpendSkillPoint();
            pistolPrefab.advancedSettings.spreadAngle -= 0.1f;
        }
    }
    public void AddAmmoPowPointRifle()
    {
        ammoPowPointsRifle += 1;
        SpendSkillPoint();
        riflePrefab.damage += 1;
    }
    public void AddAccuracyPointRifle()
    {
        if (accuracyPointsRifle >= 4)
        {
            return;
        }
        else
        {
            accuracyPointsRifle += 1;
            SpendSkillPoint();
            riflePrefab.advancedSettings.spreadAngle -= 0.1f;
        }
    }
    public void AddAmmoPowPointGrenade()
    {
        ammoPowPointsGrenade += 1;
        SpendSkillPoint();
        grenadePrefab.damage += 5;
    }
    public void AddForcePointGrenade()
    {
        if (grenadePrefab.projectileLaunchForce >= 900f)
        {
            return;
        }
        else
        {
            forcePointsGrenade += 1;
            SpendSkillPoint();
            grenadePrefab.projectileLaunchForce += 50;
        }
    }
    public void AddAthletePoint()
    {
        if (Controller.Instance.PlayerSpeed>=10)
        {
            return;
        }
        else
        {
            athletePoints += 1;
            SpendSkillPoint();
            AddMaxLifePoints(15);
            Controller.Instance.PlayerSpeed += 1;
            Controller.Instance.RunningSpeed += 1;
        }
        
    }

    public void AddLifePoints(int points)
    {
        if(currentLifePoints+points>playerLifePoints)
        {
            currentLifePoints = playerLifePoints;
        }
        else 
        {
            currentLifePoints += points;
        }
        
    }
    public void AddMaxLifePoints(int points)
    {
        playerLifePoints += points;
        currentLifePoints += points;
        GameObject.FindObjectOfType<LifeUiHandler>().lifeSlider.maxValue = playerLifePoints;
    }
    public void AddShieldPoints(int points)
    {
        if (currentSieldPoints + points > playerSieldPoints)
        {
            currentSieldPoints = playerSieldPoints;
            shieldEmpty = false;
        }
        else
        {
            currentSieldPoints += points;
            shieldEmpty = false;
        }

    }
    public void AddMaxShieldPoints(int points)
    {
        playerSieldPoints += points;
        currentSieldPoints += points;
        GameObject.FindObjectOfType<LifeUiHandler>().sieldSlider.maxValue = playerSieldPoints;
        shieldEmpty = false;
    }
    private void GodModeOn()
    {
        if (GodMode)
        {
            currentLifePoints = 100;
        }
    }
    public int GetTotalSpentPoints()
    {
        int i = totalSkillPointsSpent;
        return i;
    }
    private void TestSpawnPositionWarp()
    {
       transform.position = new Vector3(spawnPos.x, spawnPos.y + 5, spawnPos.z - 5);
        Debug.Log("Position set");
    }
    public void RespawnPlayerIfDead()
    {

        transform.position = FindObjectOfType<PlayerSpawnPos>().transform.position;
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].enemyHitByPlayer = false;
            enemies[i].thisEnemyHitByPlayer = false;
        }
        numberOfLives -= 1;
        currentLifePoints = playerLifePoints;
        currentSieldPoints = playerSieldPoints;
        FindObjectOfType<GameOverMenu>().playerIsDead = false;
    }
}
