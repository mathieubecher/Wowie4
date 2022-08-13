using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    private bool m_detectEnemy;
    public bool detectEnemy => m_detectEnemy;
    private List<Enemy> m_enemies;
    public List<Enemy> enemies => m_enemies;
    
    private void Start()
    {
        m_enemies = new List<Enemy>();
    }

    private void Update()
    {}

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.isTrigger || _other.gameObject.layer == LayerMask.NameToLayer("Robot")) return;
        m_enemies.Add(_other.transform.GetComponentInParent<Enemy>());
        m_detectEnemy = m_enemies.Count > 0;
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.isTrigger || _other.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;
        m_enemies.Remove(_other.transform.GetComponentInParent<Enemy>());
        m_detectEnemy = m_enemies.Count > 0;
    }
}
