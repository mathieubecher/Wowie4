using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MusicLoopManager : MonoBehaviour
{
    [SerializeField] private AudioClip m_musicLoop;
    private AudioSource m_source;
    
    void Awake()
    {
        m_source = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (!m_source.isPlaying)
        {
            Debug.Log("Play loop");
            m_source.clip = m_musicLoop;
            m_source.loop = true;
            m_source.Play();
        }
    }
}
