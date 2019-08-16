using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroySelf : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Destroy (this.gameObject, 0.1f);
	}

	// Update is called once per frame
	void Update () {
	}


}
