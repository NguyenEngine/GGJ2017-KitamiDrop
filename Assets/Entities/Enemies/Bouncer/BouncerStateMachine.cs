using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerStateMachine : MonoBehaviour {

	public IEnemyState StartState;
	public IEnemyState currentState;
	public IEnemyState nextState;

	public GameObject Bullet;
	public GameObject player;


	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");

		currentState = new IdleState();
		currentState.OnStateEntered (this.gameObject);
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		currentState.UpdateState();
	}

}
