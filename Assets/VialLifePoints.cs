using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VialLifePoints : MonoBehaviour
{
    public int lifePointsGiven;
    private PlayerStats playerStats;
    public bool increaseMaxLife;
    private void Start()
    {
        playerStats = GameObject.FindObjectOfType<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (!increaseMaxLife)
            {
                if (playerStats.currentLifePoints==playerStats.playerLifePoints)
                {
                    return;
                }
                else
                {
                    playerStats.AddLifePoints(lifePointsGiven);
                    FindObjectOfType<PickupItemAudio>().PlayPickupSound();
                    Destroy(gameObject);
                }
                
            }
            else if (increaseMaxLife)
            {
                playerStats.AddMaxLifePoints(lifePointsGiven);
                FindObjectOfType<PickupItemAudio>().PlayPickupSound();
                Destroy(gameObject);
            }
        }
    }
}
