using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {
	
	private GameObject m_player;
	private float transitionAlpha;
	public float lookAheadDistance;

	// Use this for initialization
	void Start () {
		m_player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		GoToTarget ();	
	}

	Vector3 targetPosition()
	{
		Vector2 playerPos = new Vector2 (m_player.transform.position.x, m_player.transform.position.y);
		Vector2 lookDir = m_player.GetComponent<PlayerInput> ().m_lastAimInput;
		Vector2 targetPosition2D = playerPos + lookDir * lookAheadDistance;
		return new Vector3 (targetPosition2D.x, targetPosition2D.y, transform.position.z);

	}

	void GoToTarget()
	{

		transform.position = Vector3.Lerp (transform.position, targetPosition(), Time.deltaTime);

	}



}
