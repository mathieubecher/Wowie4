using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<SceneAsset> m_orderedLevels;

    private int m_currentLevel = 0;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {}
    
    void Update()
    {}

    public void LoadNextLevel()
    {
        if(m_currentLevel > m_orderedLevels.Count)
        {
            m_currentLevel = 0;
        }

        SceneManager.LoadScene(m_orderedLevels[m_currentLevel].name);
        m_currentLevel++;
    }
}
