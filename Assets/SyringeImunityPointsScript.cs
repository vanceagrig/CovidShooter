using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeImunityPointsScript : MonoBehaviour
{
    public int skillPointsGiven;
    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = GameObject.FindObjectOfType<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            playerStats.AddAvailableSkillPoints(skillPointsGiven);
            FindObjectOfType<PickupItemAudio>().PlayPickupSound();
            Destroy(gameObject);
        }
    }
}
