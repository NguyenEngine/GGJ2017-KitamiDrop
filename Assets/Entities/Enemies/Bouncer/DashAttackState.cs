using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttackState : IEnemyState {


	private GameObject m_owner;
	private GameObject player;
	float timer;

	public void UpdateState ()
	{
		timer -= Time.deltaTime;
		if (timer < 0) {
			GoToNextState (new ShootState ());
		}
	}

	public void OnTriggerEnter2D (Collider2D other)
	{
		throw new System.NotImplementedException ();
	}

	public void OnStateEntered (GameObject owner)
	{
		m_owner = owner;
		player = m_owner.GetComponent<BouncerStateMachine> ().player;
		teleport (GenerateOffset ());

		timer = 1f;
	}

	public void OnStateExit ()
	{
		throw new System.NotImplementedException ();
	}

	public void GoToNextState (IEnemyState nextState)
	{
		Debug.Log ("CRNT STATE BEFORE: " + m_owner.GetComponent<BouncerStateMachine> ().currentState);
		nextState.OnStateEntered (m_owner);
		m_owner.GetComponent<BouncerStateMachine> ().currentState = nextState;
	}

	void teleport(Vector3 offset)
	{
		
		Vector3 potentialPosition = player.transform.transform.position - offset;
//		Mathf.Clamp (potentialPosition.x, );
//		Mathf.Clamp(

		//m_owner.transform.position = potentialPosition;
		GoToNextState(new ShootState());
	}

	Vector3 GenerateOffset()
	{
		return new Vector3(Random.Range (2, 5), Random.Range (2, 5), 0);
 
	}
}
