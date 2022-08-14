using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class Track : MonoBehaviour
{
    [SerializeField] private List<Transform> m_points;
    [SerializeField] private bool m_loop = true;
    private List<Vector2> m_positions;
    private int m_currentPoint = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        if (m_points.Count > 0)
        {
            foreach (var point in m_points)
            {
                m_positions.Add(point.position);
            }
        }   
    }

    public Vector2 GetVelocity(Vector2 _position, float _speed)
    {
        if(m_positions.Count < 2) return Vector2.zero;
        
        Vector2 direction = m_positions[m_currentPoint] - _position;
        float distance = direction.magnitude;
        direction.Normalize();

        if (distance < _speed * Time.deltaTime)
        {
            ++m_currentPoint;
            m_currentPoint = m_currentPoint >= m_positions.Count ? 0 : m_currentPoint;
            direction = m_positions[m_currentPoint] - _position;
            distance = direction.magnitude;
            direction.Normalize();
        }

        return direction * _speed;
    }
    
    private void OnDrawGizmos()
    {
        Transform startPos = null;
        foreach (var point in m_points)
        {
            if (startPos)
            {
                Debug.DrawLine(startPos.position, point.position, Color.cyan);
            }
            startPos = point;
        }
    }
}
