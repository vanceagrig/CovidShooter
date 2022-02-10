using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    private GameObject player;
    private bool playerUsingTurret;
    private Camera turretCamera;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        turretCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerUsingTurret)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                player.SetActive(true);
                turretCamera.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                playerUsingTurret = true;
                player.SetActive(false);
                turretCamera.gameObject.SetActive(true);
            }
        }
    }
}
