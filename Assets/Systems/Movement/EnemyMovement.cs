using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : PlayerRoot 
{
    public bool m_shouldChasePlayer = true;
	public float m_movementSpeed = 5.0f;
    public float m_friction = 1.0f;
    public bool m_lookingRight;
    private Transform m_targetTransform;
    private Rigidbody2D m_rb2d;

	void Start () 
    {
        m_targetTransform = PlayerManager.Instance.m_player1.m_transform;
        m_rb2d = GetComponent<Rigidbody2D>();

    }
	
	void Update () 
    {
        if (m_shouldChasePlayer)
        {
            MoveToTarget(m_targetTransform);
        }

        m_lookingRight = m_rb2d.velocity.x > 0;
        transform.localScale = new Vector3(m_lookingRight ? 1 : -1, 1, 1);

    }

	public void MoveLeft()
	{
		m_rigidbody2D.velocity -= Vector2.right * m_movementSpeed;

	}

	public void MoveRight()
	{
		m_rigidbody2D.velocity += Vector2.right * m_movementSpeed;
	}

	public void MoveUp()
	{
		m_rigidbody2D.velocity += Vector2.up * m_movementSpeed;

	}

	public void MoveDown()
	{
		m_rigidbody2D.velocity -= Vector2.down * m_movementSpeed;

	}


	public void MoveToTarget(Transform target)
	{
		Vector2 moveDirection = (target.position - m_transform.position).normalized;

		m_rigidbody2D.velocity +=  moveDirection * m_movementSpeed * Time.deltaTime;
        m_rigidbody2D.velocity -= m_rigidbody2D.velocity * m_friction * Time.deltaTime;
	}

	public void Patrol(Transform[] PatrolNodes, float DistanceConsideredReached)
	{
		
	}
}
