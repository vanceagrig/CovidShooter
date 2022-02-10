using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Target : MonoBehaviour
{
    public float health = 5.0f;
    public int pointValue;
    private float initialHealth;
    public ParticleSystem DestroyedEffect;

    [Header("Audio")]
    public RandomPlayer HitPlayer;
    public AudioSource IdleSource;
    
    public bool Destroyed => m_Destroyed;
    private Enemy enemyStats;
    bool m_Destroyed = false;
    [SerializeField] public float m_CurrentHealth;

    void Awake()
    {
        Helpers.RecursiveLayerChange(transform, LayerMask.NameToLayer("Target"));
        initialHealth = health;
        enemyStats = gameObject.GetComponent<Enemy>();
    }

    void Start()
    {
        if(DestroyedEffect)
            PoolSystem.Instance.InitPool(DestroyedEffect, 5);
        
        m_CurrentHealth = health;
        if(IdleSource != null)
            IdleSource.time = Random.Range(0.0f, IdleSource.clip.length);
    }
    private void Update()
    {
        bool thisEnemyHitByPlayer = enemyStats.thisEnemyHitByPlayer;
        if (m_CurrentHealth<initialHealth && thisEnemyHitByPlayer)
        {
            enemyStats.enemyHitByPlayer = true;
        }
    }

    public void Got(float damage)
    {
        m_CurrentHealth -= damage;
        
        if(HitPlayer != null)
            HitPlayer.PlayRandom();
        
        if(m_CurrentHealth > 0) { return;}
            

        Vector3 position = transform.position;
        
        //the audiosource of the target will get destroyed, so we need to grab a world one and play the clip through it
        if (HitPlayer != null)
        {
            var source = WorldAudioPool.GetWorldSFXSource();
            source.transform.position = position;
            source.pitch = HitPlayer.source.pitch;
            source.PlayOneShot(HitPlayer.GetRandomClip());
        }

        if (DestroyedEffect != null)
        {
            var effect = PoolSystem.Instance.GetInstance<ParticleSystem>(DestroyedEffect);
            effect.time = 0.0f;
            effect.Play();
            effect.transform.position = position;
        }
        if (GetComponent<Enemy>().isBoss)
        {
            GetComponent<Enemy>().bossLifeSlider.GetComponentInParent<CanvasGroup>().alpha = 0;
        }
        GetComponent<BossItemDrop>().enemyIsDead = true;
        m_Destroyed = true;
        StartCoroutine(DestroyThisEnemy());
        GameSystem.Instance.TargetDestroyed(pointValue);
    }
    private IEnumerator DestroyThisEnemy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
