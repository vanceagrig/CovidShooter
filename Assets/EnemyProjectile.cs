using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int DMGValue;
    private Transform playerTarget;
    private PlayerStats playerStats;
    private WaveSpawner waveSpawner;
    private void Awake()
    {
        playerTarget = GameObject.FindWithTag("Player").transform;
        playerStats = playerTarget.GetComponent<PlayerStats>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        int w_num = waveSpawner.waveNumber;
        int dmg = DMGValue+ (w_num);
        if (other.tag.Equals("Player"))
        {

            GetComponent<RandomPlayer>().PlayRandom();
            playerStats.GiveDMGToPlayer(dmg);
            playerStats.dmgTakenAudioPlayer.PlayHitAudio();
            Destroy(gameObject);
            
        }
        else if (other.tag.Equals("Ground"))
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(DestroyProjectileAfterInterval());
        }
    }
    private IEnumerator DestroyProjectileAfterInterval()

    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
