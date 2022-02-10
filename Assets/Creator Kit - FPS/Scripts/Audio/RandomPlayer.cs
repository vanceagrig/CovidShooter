using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomPlayer : MonoBehaviour
{
    public AudioClip[] Clips;
    public bool autoPlayThisSounds;
    private bool soundPlayed;
    public float PitchMin = 1.0f;
    public float PitchMax = 1.0f;
    
    public AudioSource source => m_Source;

    AudioSource m_Source;

    void Awake()
    {
        m_Source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(autoPlayThisSounds&&!soundPlayed)
        {
            soundPlayed = true;
            PlayRandom();
            StartCoroutine(DelayBetweenSounds());
        }
    }
    public AudioClip GetRandomClip()
    {
        return Clips[Random.Range(0, Clips.Length)];
    }

    public void PlayRandom()
    {
        if(Clips.Length == 0)
            return;
        
        PlayClip(GetRandomClip(), PitchMin, PitchMax);
    }

    public void PlayClip(AudioClip clip, float pitchMin, float pitchMax)
    {
        m_Source.pitch = Random.Range(pitchMin, pitchMax);
        m_Source.PlayOneShot(clip);
    }
    private IEnumerator DelayBetweenSounds()
    {
        yield return new WaitForSeconds(5);
        soundPlayed = false;
    }
}
