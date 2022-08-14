using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Enemy> m_enemys;
    // Start is called before the first frame update
    void Awake()
    {
        m_enemys = FindObjectsOfType<Enemy>().ToList();
    }

    private void OnEnable()
    {
        Enemy.OnDead += EnemyDead;
    }
    private void OnDisable()
    {
        Enemy.OnDead -= EnemyDead;
    }

    private void EnemyDead(Enemy _enemy)
    {
        m_enemys.Remove(_enemy);
        if (m_enemys.Count == 0)
        {
            Debug.Log("No more enemies");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
