using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState {

	private GameObject bulletA;
	private GameObject m_owner;
	private float m_currentCountDown;
	GameObject player;
	public float playerDistanceToTeleport = 10f;

	public void UpdateState()
	{
		m_currentCountDown -= Time.deltaTime;
		if (m_currentCountDown <= 0) {
			
			GoToNextState (new ShootState());
		}
			
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
	}	

	public void OnStateEntered(GameObject owner)
	{
		m_owner = owner;
		player = m_owner.GetComponent<BouncerStateMachine> ().player;
		m_currentCountDown = Random.Range (1, 3);
		
	}

	public void OnStateExit()
	{
		throw new System.NotImplementedException();
	}

	public void GoToNextState(IEnemyState nextState)
	{
		Vector2 distanceVector = m_owner.transform.position - player.transform.position;
		float distance = distanceVector.magnitude;

		if (distance > playerDistanceToTeleport) {
			nextState = new DashAttackState();
		}

		nextState.OnStateEntered (m_owner);

		m_owner.GetComponent<BouncerStateMachine> ().currentState = nextState;
	}



}
