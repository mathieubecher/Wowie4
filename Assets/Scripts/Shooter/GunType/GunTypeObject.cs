using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTypeObject : MonoBehaviour
{
    [SerializeField] protected GunType m_gunType;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            Robot robot = FindObjectOfType<Robot>();
            robot.SetGunType(m_gunType);
            Destroy(gameObject);
        }
    }
}
