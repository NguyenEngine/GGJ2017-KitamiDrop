using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
    public Vector2 m_targetDirection = new Vector2(1000,1000);
    public float m_flightSpeed = 10f;
    private Rigidbody2D m_rigidbody2D;
    private Transform m_transform;

	void Start() 
    {
        m_transform = transform;
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_transform.localScale *= Random.Range(1.0f, 1.5f);
	}
	
	void FixedUpdate() 
    {
        if (m_targetDirection.x >= 1000)
        {
            m_rigidbody2D.MovePosition(m_rigidbody2D.position + (Vector2)m_transform.up * m_flightSpeed * Time.deltaTime);
        }
        else
        {
            m_rigidbody2D.MovePosition(m_rigidbody2D.position + m_targetDirection * m_flightSpeed * Time.deltaTime);
        }
	}

    void OnCollisionEnter2D(Collision2D col2d)
    {
        if (gameObject.tag == "LaserBullet")
        {
            if (col2d.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                EventManager.Instance.PostImmediately(new OnBulletHitsEnemyEvent(transform.position));
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (col2d.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                EventManager.Instance.PostImmediately(new OnBulletHitsEnemyEvent(transform.position));
            }
            Destroy(gameObject);
        }

    }
}
