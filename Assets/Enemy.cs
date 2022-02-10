using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float sightRange = 10f , fireInterval =1f;
    public float closeRange = 3f;
    [SerializeField] bool playerInSightRange, playerInCloseRange;
    public LayerMask whatIsPlayer,whatIsGround;
    [SerializeField]Transform playerTarget;
    public GameObject bulletPrefab;
    public Transform bulletOrigin;
    private bool projectileFired,dmgPerSecDealt,playerTargetAquired;
    private PlayerStats playerStats;
    private Target enemyStats;
    public bool enemyHitByPlayer;
    public float enemySpeed = 3f;
    public bool targetSet;
    public int closeDMGPerSec = 3;
    private NavMeshAgent thisAi;
    [Space]
    private bool walkPointSet,onNavMesh;
    private Vector3 walkPoint, lastWalkPoint;
    [Space]public float walkPointRange;
    [Space]public bool isBoss;
    public Slider bossLifeSlider;
    public bool thisEnemyHitByPlayer;
    [Space] 
    [Range(0, 100)] public float alertRange;
    private WaveSpawner waveSpawner;
    private bool nearbyEnemiesNotified,distanceChecked;
    private float distCheckInterval = 10f;
    [SerializeField] private GameObject mapIcon;
    // Start is called before the first frame update

    private void Start()
    {
        playerStats = GameObject.FindObjectOfType<PlayerStats>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
        enemyStats = gameObject.GetComponent<Target>();
        thisAi = gameObject.GetComponent<NavMeshAgent>();
        if (isBoss)
        {

            bossLifeSlider = GameObject.FindObjectOfType<BossUiScript>().GetComponentInChildren<Slider>();
        }
        mapIcon = GetComponentInChildren<Canvas>().gameObject;
        mapIcon.layer = 15;
    }
    
    // Update is called once per frame
    private void Update()
    {
        onNavMesh = thisAi.isOnNavMesh;
        
        if (!distanceChecked)
        {
            distanceChecked = true;
            StartCoroutine(CheckDistanceToPlayer());
        }
        if (playerInSightRange || thisEnemyHitByPlayer || enemyHitByPlayer)
        {
            playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if(!playerInSightRange && !thisEnemyHitByPlayer && !enemyHitByPlayer)
        {
            playerTarget = null;
            Patrolling();
        }
        if (playerInCloseRange)
        {
            if (!dmgPerSecDealt)
            {
                dmgPerSecDealt = true;
                StartCoroutine(GiveDMGPerSecToPlayer());
            }
        }
        if (playerTarget!=null)
        {
            //transform.LookAt(playerTarget);
            if (isBoss)
            {
                bossLifeSlider.value = gameObject.GetComponent<Target>().m_CurrentHealth;
            }
            
            if (!targetSet)
            {
                targetSet = true;
                thisAi.SetDestination(playerTarget.position);
                if (isBoss)
                {
                    GetBossLifeSliderInfo();
                }
            }
            if (targetSet && thisAi.remainingDistance<=150)
            {
                thisAi.SetDestination(playerTarget.position);
            }
            if (!projectileFired)
            {
                projectileFired = true;
                fireInterval = Random.Range(1f,3f);
                StartCoroutine(FireProjectile());
            }
            if (thisEnemyHitByPlayer && !targetSet)
            {
                StartCoroutine(EmenyHitByPlayerCoolDown());
            }
        }
        if (enemyHitByPlayer)
        {

            for (int j = 0; j < waveSpawner.spawnedEnemies.Count; j++)
            {
                if (waveSpawner.spawnedEnemies[j] != null)
                {
                    float dist = Vector3.Distance(transform.position, waveSpawner.spawnedEnemies[j].transform.position);
                    if (dist < alertRange)
                    {
                        if (!nearbyEnemiesNotified)
                        {
                            waveSpawner.spawnedEnemies[j].GetComponent<Enemy>().enemyHitByPlayer = true;
                        }
                    }
                }
            }   
        }        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, closeRange);
    }
    private IEnumerator CheckDistanceToPlayer()
    {
        yield return new WaitForSeconds(distCheckInterval);
        {
            float distance = Vector3.Distance(transform.position, playerStats.transform.position);
            if (distance < sightRange && distance > closeRange)
            {
                playerInSightRange = true;
            }
            else if (distance < sightRange && distance < closeRange)
            {
                playerInCloseRange = true;
            }
            else
            {
                playerInCloseRange = false;
                playerInSightRange = false;
            }
        }
        distanceChecked = false;
    }
    private IEnumerator GiveDMGPerSecToPlayer()
    {
        
        yield return new WaitForSeconds(1);
        playerStats.GiveDMGToPlayer(closeDMGPerSec);
        dmgPerSecDealt = false;
    }
    private IEnumerator FireProjectile()
    {
        if(isBoss)
        {
            GameObject ball = Instantiate(bulletPrefab, bulletOrigin.transform.position, bulletOrigin.transform.rotation);
            ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 250f, 0));
            ball.GetComponent<Rigidbody>().AddForce(playerTarget.position - transform.position, ForceMode.Impulse);
            yield return new WaitForSeconds(fireInterval);
            projectileFired = false;
        }
        else
        {
            GameObject ball = Instantiate(bulletPrefab, bulletOrigin.transform.position, bulletOrigin.transform.rotation);
            ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 200f, 0));
            ball.GetComponent<Rigidbody>().AddForce(playerTarget.position - transform.position, ForceMode.Impulse);
            yield return new WaitForSeconds(fireInterval);
            projectileFired = false;
        }
        
    }
    public void Patrolling()
    {
        //float distanceToWalkPoint = Vector3.Distance(gameObject.transform.position, walkPoint);
        if (walkPointSet == false)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            if (onNavMesh)
            {
                thisAi.SetDestination(walkPoint);
        
                // Walkpoint reached
                if (thisAi.remainingDistance <= 5f)
                {
                    walkPointSet = false;
                }
                //Walkpoint reached
            }
        }
    }
    private void SearchWalkPoint()
    {
        //calculate random search point
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 20f, whatIsGround))
        {
            walkPointSet = true;
            lastWalkPoint = walkPoint;
        }
        
    }
    public void GetBossLifeSliderInfo()
    {
        bossLifeSlider.maxValue = gameObject.GetComponent<Target>().m_CurrentHealth;
        bossLifeSlider.value = gameObject.GetComponent<Target>().m_CurrentHealth;
        bossLifeSlider.GetComponentInParent<CanvasGroup>().alpha = 1;
    }
    private IEnumerator EmenyHitByPlayerCoolDown()
    {
        yield return new WaitForSeconds(5);
        thisEnemyHitByPlayer = false;
    }
}
