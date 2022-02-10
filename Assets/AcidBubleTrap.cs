using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBubleTrap : MonoBehaviour
{
    public ParticleSystem[] vFX;
    [Space]
    public int trapDMG;
    [Space]
    public AudioClip growingSFX;
    public AudioClip explosionSFX;
   private PlayerStats playerStats;
   private bool playerInTrap,trapStarted;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerStats = FindObjectOfType<PlayerStats>();
        for (int i = 0; i < vFX.Length; i++)
        {
            vFX[i].Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTrap && !trapStarted)
        {
            trapStarted = true;
            StartCoroutine(StartTrapSequence());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            playerInTrap = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            playerInTrap = false;
        }
    }
    private IEnumerator StartTrapSequence()
    {
        vFX[0].Play();
        audioSource.PlayOneShot(growingSFX);
        yield return new WaitForSeconds(3f);
        float dist = Vector3.Distance(transform.position, playerStats.GetComponent<Transform>().position);
        if(dist<15f)
        {
            playerStats.GiveDMGToPlayer(trapDMG);
        }
        audioSource.PlayOneShot(explosionSFX);
        vFX[1].Play();
        vFX[0].Stop();
        yield return new WaitForSeconds(0.2f);
        trapStarted = false;
    }
}
