using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {

	private Transform respawnTransform;
	[SerializeField]
	private GameObject deathBody;
	// Is the player in Ghost mode to retrieve it's body?
	public bool isGhost;

	private const float MAXRETRIEVETIME = 5f;

	[SerializeField]
	private float currentRetrieveTime;
	
	void Start () {

		currentRetrieveTime = MAXRETRIEVETIME;
		respawnTransform = GameObject.FindGameObjectWithTag("Respawn").transform;
		//add the death method to the onzerohealthdelegate
		GetComponent<LifeScript> ().OnDeath += StartGhostMode;
	}

	void Update()
	{
		if (isGhost) {
			currentRetrieveTime -= Time.deltaTime;
		}
	}

	private void Ressurrect()
	{
		isGhost = false;
	}

	void StartGhostMode()
	{
		if (!isGhost) 
		{
			GameObject corpse = GameObject.Instantiate (deathBody, transform.position, Quaternion.identity);
			transform.position = respawnTransform.position;
			isGhost = true;
			Debug.Log ("dood");
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.layer == LayerMask.NameToLayer("Enemy") && isGhost) 
		{
 			
		}
	}



}
