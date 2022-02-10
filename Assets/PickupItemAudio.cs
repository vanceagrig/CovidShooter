using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItemAudio : MonoBehaviour
{
    [SerializeField]private AudioSource audioSource;
    public AudioClip[] pickupAudioClips;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPickupSound()
    {
        int i = Random.Range(0, pickupAudioClips.Length);
        audioSource.PlayOneShot(pickupAudioClips[i]);
    }
}
