using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunType/GunType", order = 1)]
public class GunType : ScriptableObject
{
    [SerializeField] protected float m_firingRate = 1f;
    [SerializeField] protected GameObject m_bullet;
  
    protected float m_timer = 0f;

    public virtual bool Shoot(Vector3 _startPos, float _angle, bool _isRight, string _bulletLayer)
    {
        m_timer += Time.deltaTime;
        if(m_timer >= m_firingRate)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, (_isRight ? 1f : -1f ) * _angle);
            InstantiateBullet(_startPos, rotation, _isRight, _bulletLayer);
            return true;
        }
        return false;
    }

    protected void InstantiateBullet(Vector3 _startPos, Quaternion _rotation, bool _isRight, string _bulletLayer)
    {
        GameObject spawnedObject = Instantiate(m_bullet, _startPos, _rotation);
        Vector3 currentLocalScale = spawnedObject.transform.localScale;
        spawnedObject.transform.localScale = new Vector3((_isRight ? 1f : -1f ) * currentLocalScale.x, currentLocalScale.y, currentLocalScale.z);
        spawnedObject.layer = LayerMask.NameToLayer(_bulletLayer);
    }
}
