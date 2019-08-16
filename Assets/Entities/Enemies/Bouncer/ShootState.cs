using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : IEnemyState {

	ShootingPatterns m_shooter;
	[SerializeField]
	private GameObject m_bulletPrefab;
	private GameObject m_owner;
	private int m_shootCount;
	private int m_maxShootCount = 3;
	private float timer;
	public float max_timer = 1.5f;
	public float playerDistanceToTeleport = 8;

	public void UpdateState ()
	{
		timer -= Time.deltaTime;
		if (timer < 0 && m_shootCount < m_maxShootCount) {
			Shoot ();
			
		} 
		else if (m_shootCount >= m_maxShootCount) 
		{
			GoToNextState (new IdleState());
		}
	}

	public void OnTriggerEnter2D (Collider2D other)
	{
		
	}

	public void OnStateEntered (GameObject Owner)
	{
		m_shootCount = 0;
		m_owner = Owner;
		timer = max_timer;
		m_bulletPrefab = m_owner.GetComponent<BouncerStateMachine>().Bullet;
		m_shooter = m_owner.GetComponent<ShootingPatterns>();

	}

	public void OnStateExit ()
	{
		
	}
	 
	public void GoToNextState (IEnemyState nextState)
	{
		Vector3 distanceVector = m_owner.transform.position - GameObject.FindWithTag("Player").transform.position;
		distanceVector.z = 0;
		float distance = distanceVector.magnitude;

	if (distance > playerDistanceToTeleport) {
			nextState = new DashAttackState();
		}
		nextState.OnStateEntered(m_owner);

		m_owner.GetComponent<BouncerStateMachine>().currentState = nextState;
	}


	private void Shoot()
	{

		m_shootCount++;
		int random = Random.Range (1, 3);


		Vector2 m_toPlayerDirection = (m_owner.GetComponent<BouncerStateMachine>().player.transform.position - m_owner.GetComponent<Transform> ().position);

		float angleBetweenPlayer = Mathf.Atan2 (m_toPlayerDirection.x, m_toPlayerDirection.y);
		timer = max_timer;

		switch (random)
		{
		case 1:
			m_shooter.ShootCone (m_bulletPrefab, angleBetweenPlayer);
			break;
		case 2:
			m_shooter.ShootRadial (m_bulletPrefab);
			break;
		case 3:
			m_shooter.ShootHalfCircle (m_bulletPrefab, angleBetweenPlayer);
			break;

		}

	}


}
