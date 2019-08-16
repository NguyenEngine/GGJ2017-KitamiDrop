using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : WeaponBase 
{
    public float m_firingSpeed = 10.0f;
    public float m_scaleOffsetMin = 2.7f;
    public float m_scaleOffsetMax = 4.8f;

    public override IEnumerator FireWeapon()
    {
        m_isFiring = true;
        while (m_isFiring)
        {
            FireBullet();
            yield return StartCoroutine(Wait(1.0f / m_firingSpeed));
        }
    }

    public override IEnumerator StopFiring()
    {
        m_isFiring = false;
        yield return null;
    }

    private void FireBullet()
    {
        Bullet shot = PlayerManager.Instance.m_player1.m_playerShooting.Shoot(m_bulletPrefab, PlayerManager.Instance.m_player1.m_playerInput.m_lastAimInput);
        if (shot)
        {
            shot.transform.localScale *= Random.Range(m_scaleOffsetMin, m_scaleOffsetMax);
        }
    }
}
