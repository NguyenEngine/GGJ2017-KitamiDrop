using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVolume : MonoBehaviour {
    
	[SerializeField]
	private int m_damage;
	[SerializeField]
	bool doDamageOnStay = false;
	[SerializeField]
	int m_DPSonStay;  

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.GetComponent<LifeScript>() != null) 
		{
			col.gameObject.GetComponent<LifeScript>().ApplyDamage(transform.position, m_damage);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.GetComponent<LifeScript>() != null) 
		{
			col.gameObject.GetComponent<LifeScript>().ApplyDamage(transform.position, m_damage);
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.GetComponent<LifeScript>() != null && doDamageOnStay) 
		{
			Debug.Log (this.gameObject + " has dealth damage: " + m_DPSonStay);
			
			col.gameObject.GetComponent<LifeScript>().ApplyDamage(transform.position, m_DPSonStay);
		}
	}

}
