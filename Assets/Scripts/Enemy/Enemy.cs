using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{   
    private Rigidbody2D m_rigidbody;
    private LifeManager m_lifeManager;
    private AudioSource m_audio;
    
    public LifeManager lifeManager => m_lifeManager;
    public AudioSource audio => m_audio;
    
    [Header("Sound")]
    [SerializeField] private List<AudioClip> m_hitSoundsAtStart;
    
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_lifeManager = GetComponent<LifeManager>();
        m_audio = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        lifeManager.OnHit += Hit;
    }

    private void OnDisable()
    {
        lifeManager.OnHit -= Hit;
    }

    private void Hit(Vector2 _origin, float _damage, bool _dead)
    {
        int randomSoundId = (int)math.floor(Random.Range(0, m_hitSoundsAtStart.Count));
        audio.PlayOneShot(m_hitSoundsAtStart[randomSoundId]);
    }
}
