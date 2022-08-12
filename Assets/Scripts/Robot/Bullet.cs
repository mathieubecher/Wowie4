using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_damage;
    [SerializeField] private float m_speed;

    private Rigidbody2D m_rigidbody;
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody.velocity = transform.forward * m_speed;
    }

    // Update is called once per frame
    void Update()
    {}

    public void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
}
