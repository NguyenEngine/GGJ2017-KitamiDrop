using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState{

	void UpdateState();
	void OnTriggerEnter2D(Collider2D other);
	void OnStateEntered(GameObject owner);
	void OnStateExit();
	void GoToNextState(IEnemyState nextState);


}
