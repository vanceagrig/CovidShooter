using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SieldPointsScript : MonoBehaviour
{
    public int shieldPointsGiven;
    private PlayerStats playerStats;
    public bool increaseMaxSield;
    private void Start()
    {
        playerStats = GameObject.FindObjectOfType<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (!increaseMaxSield)
            {
                if (playerStats.currentSieldPoints==playerStats.playerSieldPoints)
                {
                    return;
                }
                else
                {
                    playerStats.AddShieldPoints(shieldPointsGiven);
                    FindObjectOfType<PickupItemAudio>().PlayPickupSound();
                    Destroy(gameObject);
                }
                
            }
            else if (increaseMaxSield)
            {
                playerStats.AddMaxShieldPoints(shieldPointsGiven);
                FindObjectOfType<PickupItemAudio>().PlayPickupSound();
                Destroy(gameObject);
            }
        }
    }
}
