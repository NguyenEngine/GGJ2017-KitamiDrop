using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBulletType : MonoBehaviour {

	public float yScaleAlpha;
	public float growthSpeed;
	public float maxYscale = 100;
	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3 (transform.localScale.x, 0, 1);

		yScaleAlpha = 0;
	}
	
	// Update is called once per frame
	void Update () {

		GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		if (yScaleAlpha <= 1) 
		
		{
			yScaleAlpha += Time.deltaTime * growthSpeed;
		}

		transform.localScale = new Vector3 (transform.localScale.x, (Mathf.Lerp (transform.localScale.y, maxYscale, yScaleAlpha)), 1);


	}
}
