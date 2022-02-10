using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnLocation;
    public int spawnNumber;
    public List<GameObject> spawnedEnemies;
    public bool spawnOneTime;
    private bool spawned,targetAquired;
    private Vector3 spawnPos;
    // Start is called before the first frame update
    private void Start()
    {
        spawnPos = spawnLocation.position;
    }
    private void Update()
    {
        if (!targetAquired)
        {
            for (int i = 0; i < spawnedEnemies.Count; i++)
            {
                if (spawnedEnemies[i].GetComponent<Enemy>().enemyHitByPlayer == true)
                {
                    targetAquired = true;
                    for (int j = 0; j < spawnedEnemies.Count; j++)
                    {
                        spawnedEnemies[j].GetComponent<Enemy>().enemyHitByPlayer = true;
                    }
                }
            }
        }
        if (spawnedEnemies.Count>0)
        {
            for (int i = 0; i < spawnedEnemies.Count; i++)
            {
                if(spawnedEnemies[i] == null)
                {
                    spawnedEnemies.Remove(spawnedEnemies[i]);
                }
                
            }
        }
    }
    public void SpawnEnemies()
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            spawnPos = new Vector3(spawnPos.x + Random.Range(1, 5), spawnPos.y, spawnPos.z + Random.Range(1, 5));
            GameObject obj = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            spawnedEnemies.Add(obj);
            spawnPos = spawnLocation.position;
        }
    }
    public void FinishSpecialEvent()
    {
        for(int i = 0; i<spawnedEnemies.Count; i++)
        {
            Destroy(spawnedEnemies[i]);
            spawnedEnemies.Remove(spawnedEnemies[i]);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (spawnOneTime)
            {
                if (!spawned)
                {
                    spawned = true;
                    if (spawnNumber > 0)
                    {

                        SpawnEnemies();

                    }
                }
            }
            else if ((!spawnOneTime))
            {
                if (spawnNumber > 0)
                {
                    for (int i = 0; i < spawnNumber; i++)
                    {
                        SpawnEnemies();
                    }
                }
            }
        }
    }
}
