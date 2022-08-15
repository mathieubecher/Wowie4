using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SceneAsset m_levelScene;
    [SerializeField] private SceneAsset m_customizeScene;
    [SerializeField] private List<SceneAsset> m_orderedLevels;

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

        SceneManager.LoadScene(m_orderedLevels[m_currentLevel].name);
        m_currentLevel++;
    }

    public void LoadNextLevel(GameManager _manager)
    {
        LoadNextLevel();
    }

    public void LoadLevelScene()
    {
        isOnCustomize = false;
        SceneManager.LoadScene(m_levelScene.name);
    }

    public void LoadCustomizeScene()
    {
        isOnCustomize = true;
        SceneManager.LoadScene(m_customizeScene.name);
    }

    public void LoadLevel(SceneAsset _level)
    {
        isOnCustomize = m_customizeScene.name == _level.name ? true : false;
        SceneManager.LoadScene(m_orderedLevels.Find(x => x.name == _level.name).name);
    }
}
