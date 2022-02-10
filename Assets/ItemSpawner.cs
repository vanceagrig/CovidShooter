using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefab;
    public Transform spawnLocation;
    public int spawnNumber;
    public List<GameObject> spawnedItems;
    public bool itemsSpawned,countDown;
    private Vector3 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        itemsSpawned = true;
        spawnPos = spawnLocation.position;
        SpawnItems();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!itemsSpawned)
        {
            itemsSpawned = true;
            countDown = true;
            StartCoroutine(SpawnNextWaveOfItems());
        }
        if (itemsSpawned)
        {
            CheckForMissingItems();
        }
        if (spawnedItems.Count==0&&itemsSpawned&&!countDown)
        {
            itemsSpawned = false;
        }
    }
    private void SpawnItems()
    {
        
        for (int i = 0; i < spawnNumber; i++)
        {
            for (int j = 0; j < itemPrefab.Length; j++)
            {
                spawnPos = new Vector3(spawnPos.x + Random.Range(1, 5), spawnPos.y, spawnPos.z + Random.Range(1, 5));
                GameObject obj = Instantiate(itemPrefab[j], spawnPos, Quaternion.identity);
                spawnedItems.Add(obj);
                spawnPos = spawnLocation.position;
            }
        }
        
    }
    IEnumerator SpawnNextWaveOfItems()
    {
        
        yield return new WaitForSeconds(20);
        countDown = false;
        SpawnItems();
    }
    private void CheckForMissingItems()
    {
        for (int i =0;i<spawnedItems.Count;i++)
        {
            if(spawnedItems[i]==null)
            {
                spawnedItems.Remove(spawnedItems[i]);
            }
        }
    }
}
