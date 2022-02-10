using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidScript : MonoBehaviour
{
    public PlayerStats playerStats;
    public int acidDamagePerSec;
    public bool damageDealt,playerInAcid;
    public Vector3 currentGravity;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInAcid&&!damageDealt)
        {
            damageDealt = true;
            StartCoroutine(DealAcidDamage());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            playerInAcid = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            playerInAcid = false;
        }
    }
   
    private IEnumerator DealAcidDamage()
    {
        playerStats.GiveDMGToPlayer(acidDamagePerSec);
        playerStats.dmgTakenAudioPlayer.PlayAcidHitAudio();
        yield return new WaitForSeconds(1);
        damageDealt = false;
    }

}
