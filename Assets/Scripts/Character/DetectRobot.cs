using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRobot : MonoBehaviour
{
    public bool disable;
    public float alpha;
    
    
    [SerializeField] private float m_maxDist = 5.0f;
    [SerializeField] private Color m_reachableColor;
    [SerializeField] private Color m_unreachableColor;
    [SerializeField] private AnimationCurve m_alphaByDist;

    private LineRenderer m_line;
    private bool m_detectRobot;
    private Robot m_robotRef;
    public bool detectRobot => m_detectRobot && CanReachRobot();
    public Robot robotRef => m_robotRef;
    public float maxDist => m_maxDist;

    private void Awake()
    {
        m_line = GetComponent<LineRenderer>();
        m_maxDist = GetComponent<CircleCollider2D>().radius;
    }

    private void LateUpdate()
    {
        m_line.enabled = m_detectRobot;
        if (m_line.enabled)
        {
            int lenght = m_line.positionCount;
            Color color = detectRobot ? m_reachableColor : m_unreachableColor;
            color.a *= m_alphaByDist.Evaluate((m_robotRef.transform.position - transform.position).magnitude / m_maxDist) * alpha;
            
            m_line.endColor = color;
            m_line.startColor = m_line.endColor;
            Vector3[] points = new Vector3[lenght];
            Vector3 start = transform.position + Vector3.up;
            Vector3 stop = m_robotRef.transform.position+ Vector3.up * 0.4f;
            for (int i = 0; i < lenght; ++i)
            {
                points[i] = Vector3.Lerp(start, stop, i / (float)lenght);
            }
            m_line.SetPositions(points);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.isTrigger || _other.gameObject.layer == LayerMask.NameToLayer("Character")) return;
        m_robotRef = _other.transform.GetComponentInParent<Robot>();
        m_detectRobot = true;
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.isTrigger || _other.gameObject.layer != LayerMask.NameToLayer("Robot")) return;
        m_robotRef = null;
        m_detectRobot = false;
    }

    private bool CanReachRobot()
    {
        if (!m_detectRobot) return false;
        
        Vector2 direction = robotRef.transform.position - transform.position;
        Vector2 size = new Vector2(1.0f, 2.0f);
        Vector2 origin = transform.position + Vector3.up * size.y / 2f;
        float distance = direction.magnitude;
        direction.Normalize();
        LayerMask mask = LayerMask.GetMask("Default");
        
        RaycastHit2D hitInfo = Physics2D.BoxCast(origin, size * 0.95f, 0.0f, direction: direction, distance: distance, mask);
        if (hitInfo)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            return false;
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + (Vector3)direction * distance, Color.cyan);
            return true;
        }
    }
}
