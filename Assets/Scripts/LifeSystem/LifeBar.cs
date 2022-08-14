using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    public Transform m_redBar;
    public Transform m_grayBar;

    [SerializeField] private LifeManager m_life;

    private float m_maxLife;
    // Start is called before the first frame update
    void Awake()
    {
        m_maxLife = m_life.life;
        
    }

    // Update is called once per frame
    void Update()
    {
        var localScale = m_redBar.localScale;
        localScale = new Vector3(1.0f * math.max(m_life.life / m_maxLife, 0.0f), 1.0f, 1.0f);
        m_redBar.localScale = localScale;
        m_grayBar.localScale = localScale;
    }
}
