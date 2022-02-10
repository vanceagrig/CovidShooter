using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform Target;
    private float vectorXRotation=90;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (Target.transform.position.x, transform.position.y, Target.transform.position.z);
        transform.rotation = Quaternion.Euler(vectorXRotation, Target.rotation.y, Target.rotation.z);
    }
}
