using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickupItem : MonoBehaviour
{
    public int ammoGivenIfPickedUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (other.GetComponent<Controller>().grenadePickedUp == true)
            {
                other.GetComponent<Controller>().ChangeAmmo(2, ammoGivenIfPickedUp);
            }
            else
            {
                other.GetComponent<Controller>().grenadePickedUp = true;
            }
            Destroy(gameObject);
        }
    }
}
