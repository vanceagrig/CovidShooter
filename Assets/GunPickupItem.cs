using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickupItem : MonoBehaviour
{
    public int ammoGivenIfPickedUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if(other.GetComponent<Controller>().gunPickedUp == true)
            {
                other.GetComponent<Controller>().ChangeAmmo(0, 100);
            }
            else 
            {
                other.GetComponent<Controller>().gunPickedUp = true;
            }
            Destroy(gameObject);
        }
    }
}
