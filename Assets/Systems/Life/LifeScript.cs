using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnDamage(Vector2 position, float damage);
public delegate void OnZeroHealth();

public class LifeScript : MonoBehaviour {

    [SerializeField]
    public int maxHealth = 100;
	[SerializeField]
	private int healthPoints = 100;

	public OnZeroHealth OnDeath;
    public OnDamage OnDamage;

    public GameObject damagePPFX;
    public GameObject deathPPFX;

    public int GetHealth()
    {
        return healthPoints;
    }

    public bool IsAlive()
    {
        return healthPoints > 0;
    }

    public void ApplyDamage(Vector2 position, int damage)
	{
        if (healthPoints == 0)
            return;
        healthPoints = Mathf.Max(0, healthPoints - damage);

        float angle = Mathf.Atan2(transform.position.y - position.y, transform.position.x - position.x);
        FX.Instance.PlayPostProcessEffect(damagePPFX, Camera.main.WorldToScreenPoint(transform.position), angle);

        if (GetComponent<PlayerSound>())
        {
            GetComponent<PlayerSound>().PlaySound();
        }


        if (OnDamage != null)
            OnDamage.Invoke(position, damage);

        if (healthPoints == 0)
        {
            FX.Instance.PlayPostProcessEffect(deathPPFX, Camera.main.WorldToScreenPoint(transform.position), angle);
            if (OnDeath != null)
                OnDeath.Invoke();
        }
	}

	public void GainHealthPoints(int hp)
	{
        if (healthPoints >= maxHealth)
            return;
        healthPoints = Mathf.Min(maxHealth, healthPoints + hp);
	}

}
