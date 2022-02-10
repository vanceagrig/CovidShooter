using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitAudio : MonoBehaviour
{
    private AudioSource thisAudioSource;
    public AudioClip[] hitAudio;
    public AudioClip acidHitAudio;
    [Range(0, 0.3f)] public float volume;

    private void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();
        thisAudioSource.volume = volume;
    }
    public void PlayHitAudio()
    {
        
        int i = Random.Range(0, hitAudio.Length - 1);
        thisAudioSource.PlayOneShot(hitAudio[i]);
    }
    public void PlayAcidHitAudio()
    {
        thisAudioSource.PlayOneShot(acidHitAudio);

    }
}
