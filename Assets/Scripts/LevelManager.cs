using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int m_levelScene;
    [SerializeField] private int m_customizeScene;
    [SerializeField] private List<int> m_orderedLevels;

    private int m_currentLevel = 0;
    private bool isOnCustomize = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        GameManager.OnWin += LoadNextLevel;
    }

    private void OnDisable()
    {
        
        GameManager.OnWin -= LoadNextLevel;
    }

    void Start()
    {}
    
    void Update()
    {}

    public void LoadNextLevel()
    {
        if(!isOnCustomize)
        {
            LoadCustomizeScene();
            return;
        }
        isOnCustomize = false;

        if(m_currentLevel > m_orderedLevels.Count)
        {
            m_currentLevel = 0;
        }

        SceneManager.LoadScene(m_orderedLevels[m_currentLevel]);
        m_currentLevel++;
    }

    public void LoadNextLevel(GameManager _manager)
    {
        LoadNextLevel();
    }

    public void LoadLevelScene()
    {
        isOnCustomize = false;
        SceneManager.LoadScene(m_levelScene);
    }

    public void LoadCustomizeScene()
    {
        isOnCustomize = true;
        SceneManager.LoadScene(m_customizeScene);
    }

    public void LoadLevel(int _level)
    {
        isOnCustomize = m_customizeScene == _level ? true : false;
        SceneManager.LoadScene(m_orderedLevels.Find(x => x == _level));
    }

    public void Reload()
    {
        m_currentLevel--;
        isOnCustomize = true;
        SceneManager.LoadScene(m_customizeScene);
    }

    // reset
}
