using UnityEngine;
using System.Collections;

public class PlayerMovement2D : PlayerRoot
{
    private LifeScript m_playerLife;
    private bool m_alive;

    public float m_acceleration = 1000f;
    public float m_friction = 40f;

    void Awake()
    {
        m_playerLife = GetComponent<LifeScript>();
        m_alive = true;
    }

    void FixedUpdate()
    {
        m_alive = !m_playerLife || m_playerLife.IsAlive();

        if (m_alive)
        {
            m_rigidbody2D.velocity += m_playerInput.m_movementInput * m_acceleration * Time.deltaTime;
            m_rigidbody2D.velocity -= m_rigidbody2D.velocity * m_friction * Time.deltaTime;
        }
    }
}
