using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiflePickupItem : MonoBehaviour
{
    public int ammoGivenIfPickedUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (other.GetComponent<Controller>().riflePickedUp == true)
            {
                other.GetComponent<Controller>().ChangeAmmo(0, 100);
            }
            else
            {
                other.GetComponent<Controller>().riflePickedUp = true;
            }
            Destroy(gameObject);
        }
    }
}
