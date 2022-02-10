using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AmmoBox : MonoBehaviour
{
    [AmmoType]
    public int ammoType;
    public int amount;
    
    void Reset()
    {
        gameObject.layer = LayerMask.NameToLayer("PlayerCollisionOnly");
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        Controller c = other.GetComponent<Controller>();

        if (c != null)
        {
            if (c.GetAmmo(ammoType) <1999)
            {
                c.ChangeAmmo(ammoType, amount);
                Destroy(gameObject);
            }
            
        }
    }
}
