using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    private bool m_detectEnemy;
    public bool detectEnemy => m_detectEnemy;
    private Enemy m_enemy;
    public bool enemy => m_enemy;
    
    private void Start()
    {}

    private void Update()
    {}

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.isTrigger || _other.gameObject.layer == LayerMask.NameToLayer("Robot")) return;
        m_enemy = _other.transform.GetComponentInParent<Enemy>();
        m_detectEnemy = true;
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.isTrigger || _other.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;
        m_enemy = null;
        m_detectEnemy = false;
    }
}
