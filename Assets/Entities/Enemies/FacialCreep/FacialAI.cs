using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacialAI : MonoBehaviour {

	private LifeScript lifeScript;
	[SerializeField]GameObject Exploder;
	CircleCollider2D explodeCollider;

	// Use this for initialization
	void Start () {
		explodeCollider = GetComponentInChildren<CircleCollider2D> ();
		lifeScript = GetComponent<LifeScript> ();
		lifeScript.OnDeath += Explode;

	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	void Explode()
	{
		Instantiate (Exploder, transform.position, Quaternion.identity);
	}
}
