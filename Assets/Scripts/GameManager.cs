using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Serializable]
    private struct VictoryCondition
    {
        public bool destroyAllEnemies;
        public bool destroyAllSludges;
    }
    
    [SerializeField] private VictoryCondition m_conditions;
    
    [SerializeField] private List<Enemy> m_enemys;
    [SerializeField] private List<Sludge> m_sludges;
    [SerializeField] private List<GunBehavior> m_gunBehaviorsUnlock;
    public List<GunBehavior> gunBehaviorsUnlock => m_gunBehaviorsUnlock;

    private bool m_win;

    public delegate void OnWinDelegate(GameManager _manager);

    public static event OnWinDelegate OnWin;
    // Start is called before the first frame update
    void Awake()
    {
        m_enemys = FindObjectsOfType<Enemy>().ToList();
        m_sludges = FindObjectsOfType<Sludge>().ToList();
    }
    
    void Update()
    {
        if (!m_win)
        {
            bool win = !m_conditions.destroyAllEnemies || m_enemys.Count == 0;
            win &=     !m_conditions.destroyAllSludges || m_sludges.Count == 0;
            m_win = win;
        }
        else
        {
            OnWinCondition();
        }
    }
    
    private void OnEnable()
    {
        Enemy.OnDead += EnemyDead;
        Sludge.OnDead += SludgeDead;
    }
    private void OnDisable()
    {
        Enemy.OnDead -= EnemyDead;
        Sludge.OnDead -= SludgeDead;
    }

    private void SludgeDead(Sludge _sludge)
    {
        m_sludges.Remove(_sludge);
        if (m_sludges.Count == 0)
        {
            Debug.Log("No more ludge");
        }
    }

    private void EnemyDead(Enemy _enemy)
    {
        m_enemys.Remove(_enemy);
        if (m_enemys.Count == 0)
        {
            Debug.Log("No more enemies");
        }
    }

    private void OnWinCondition()
    {
        OnWin?.Invoke(this);
    }
}
