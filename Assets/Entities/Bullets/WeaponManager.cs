using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponManager : Singleton<WeaponManager> 
{
    private Dictionary<Type, WeaponBase> m_weaponEvents = new Dictionary<Type, WeaponBase>();
    public List<WeaponBase> m_activeWeapons = new List<WeaponBase>();

    public void AddWeaponEvent<T>(WeaponBase weapon) where T : class, IEvent
    {
        Type t = typeof(T);
        if (!m_weaponEvents.ContainsKey(t))
        {
            m_weaponEvents.Add(t, weapon);
        }
    }

    public void UpdateWeapon<T>(bool enabled) where T : class, IEvent
    {
        Type t = typeof(T);
        if (!m_weaponEvents.ContainsKey(t)) 
        {
            Debug.LogError("Current weapon is not amoungst weapon events, so will not fire target weapon.");
            return;
        }

        //Debug.LogWarning("<color=yellow>Weapon " + t.ToString() + " is now " + enabled + "</color>");
        WeaponBase foundWeapon = m_weaponEvents[t];
        if (enabled)
        {
            m_activeWeapons.Add(foundWeapon);
            StartCoroutine(foundWeapon.FireWeapon());
        }
        else
        {
            StartCoroutine(foundWeapon.StopFiring());
            m_activeWeapons.Remove(foundWeapon);
        }
    }
}

[Serializable]
public abstract class WeaponBase : MonoBehaviour
{
    public bool m_isFiring;
    public GameObject m_bulletPrefab;

    public abstract IEnumerator FireWeapon();
    public abstract IEnumerator StopFiring();

    public IEnumerator Wait(float duration)
    {
        float timerCurrent = 0.0f;
        while (timerCurrent <= duration && m_isFiring)
        {
            timerCurrent += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
