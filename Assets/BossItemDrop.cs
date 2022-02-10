using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossItemDrop : MonoBehaviour
{
    public GameObject[] itemPrefab;
    public GameObject[] rareItemPrefab;
    public bool itemsSpawned,enemyIsDead;
    private Vector3 spawnPos;
    [Range(1, 100)] public int dropChance;
    [Range(1, 100)] public int rareDropChance;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (!itemsSpawned&& enemyIsDead)
        {
            itemsSpawned = true;
            SpawnItems();
        }
    }
    private void SpawnItems()
    {

        for (int j = 0; j < itemPrefab.Length; j++)
        {
            int randomNum = Mathf.FloorToInt(Random.Range(1, 100));
            if (randomNum <= dropChance)
            {
                spawnPos = transform.position;

                spawnPos = new Vector3(spawnPos.x + Random.Range(1, 5), spawnPos.y+3, spawnPos.z + Random.Range(1, 5));

                GameObject obj = Instantiate(itemPrefab[j], spawnPos, Quaternion.identity);
            }
        }
        if (rareItemPrefab.Length>0)
        {
            for (int i = 0; i < rareItemPrefab.Length; i++)
            {
                int randomNum = Mathf.FloorToInt(Random.Range(1, 100));
                if (randomNum <= rareDropChance)
                {
                    spawnPos = transform.position;

                    spawnPos = new Vector3(spawnPos.x + Random.Range(1, 5), spawnPos.y+3, spawnPos.z + Random.Range(1, 5));

                    GameObject obj = Instantiate(rareItemPrefab[i], spawnPos, Quaternion.identity);
                }
            }
        }
    }
   
}
