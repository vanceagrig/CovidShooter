using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactDetection : MonoBehaviour
{
    public int DMGValue;
    private Transform playerTarget;
    private PlayerStats playerStats;
    private WaveSpawner waveSpawner;
    public ParticleSystem impactVFX;
    private void Awake()
    {
        playerTarget = GameObject.FindWithTag("Player").transform;
        playerStats = playerTarget.GetComponent<PlayerStats>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        int w_num = waveSpawner.waveNumber;
        int dmg = DMGValue + (w_num);
        if (other.tag.Equals("Player"))
        {
            ParticleSystem impact = Instantiate(impactVFX, transform.position, transform.rotation) as ParticleSystem;
            playerStats.GiveDMGToPlayer(dmg);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = new Vector3(playerTarget.position.x, playerTarget.position.y + 5, playerTarget.position.z - 5);
            Destroy(gameObject);
            FindObjectOfType<PlayerHitAudio>().PlayHitAudio();
        }
        else if (other.tag.Equals("Ground"))
        {
            ParticleSystem impact = Instantiate(impactVFX, transform.position, transform.rotation) as ParticleSystem;
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(DestroyProjectileAfterInterval());
        }
    }
    private IEnumerator DestroyProjectileAfterInterval()

    {
        yield return new WaitForSeconds(3);
        ParticleSystem impact = Instantiate(impactVFX, transform.position, transform.rotation) as ParticleSystem;
        Destroy(gameObject);
    }
}
