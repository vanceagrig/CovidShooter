using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RotateTowardsEnemy : MonoBehaviour
{
    public Transform pointingArrowTransform;
    private bool distanceMesured;
    public int enemiesLeft;
    public float intervalCheck;
    [SerializeField]private float shortestDistance = 0;
    [SerializeField] private Transform shortDistEnemyTransform;
    [SerializeField]private Transform playerTransform;
    [SerializeField]private WaveSpawner waveSpawner;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
        GetDistancesToPlayer();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (enemiesLeft < 10)
        {
            Debug.Log("Enemies below 10");
            if (!distanceMesured)
            {
                distanceMesured = true;
                StartCoroutine(GetDistanceAtImterval());
            }
            pointingArrowTransform.LookAt(shortDistEnemyTransform.position);
            pointingArrowTransform.gameObject.SetActive(true);
        }
        else
        {
            pointingArrowTransform.gameObject.SetActive(false);
        }
    }
    
    private void GetDistancesToPlayer()
    {
        
        for (int i = 0; i< waveSpawner.spawnedEnemies.Count; i++)
        {
            float dist = Vector3.Distance(playerTransform.position, waveSpawner.spawnedEnemies[i].GetComponent<Transform>().position);
            if(shortestDistance==0)
            {
                shortestDistance = dist;
            }
            else
            {
                if (shortestDistance>=dist)
                {
                    shortestDistance = dist;
                    shortDistEnemyTransform = waveSpawner.spawnedEnemies[i].GetComponent<Transform>();
                }
            }
        }
    }
    private IEnumerator GetDistanceAtImterval()
    {
        
        yield return new WaitForSeconds(intervalCheck);
        GetDistancesToPlayer();
        distanceMesured = false;
        shortestDistance = 0;
    }
}
