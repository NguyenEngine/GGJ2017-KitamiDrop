using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideShot : WeaponBase 
{
    public float m_scaleOffsetMin = 0.7f;
    public float m_scaleOffsetMax = 1.8f;

    public override IEnumerator FireWeapon()
    {
        m_isFiring = true;
        FireBullet();
        yield return null;
    }

    public override IEnumerator StopFiring()
    {
        m_isFiring = false;
        yield return null;
    }

    private void FireBullet()
    {
        Vector2 m_toPlayerDirection = PlayerManager.Instance.m_player1.m_playerInput.m_lastAimInput;
        float angleBetweenPlayer = Mathf.Atan2(m_toPlayerDirection.x, m_toPlayerDirection.y);
        PlayerManager.Instance.m_player1.GetComponent<ShootingPatternsPlayer>().ShootSide(m_bulletPrefab, angleBetweenPlayer);
    }
}
