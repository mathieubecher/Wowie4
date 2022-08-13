using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    private bool m_invunlerability = false;
    public void SetInvulnerability(bool _enable)
    {
        m_invunlerability = _enable;
    }

}
