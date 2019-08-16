using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : WeaponBase 
{
    public float m_firingSpeed = 5.0f;
    public float m_scaleOffsetMin = 0.7f;
    public float m_scaleOffsetMax = 1.8f;
    public float m_coneOffset = 20f; // In degrees

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
            //shot.m_targetDirection = shot.transform.TransformVector(new Vector2(Random.Range(-m_coneOffset, m_coneOffset),0));
            shot.transform.localScale *= Random.Range(m_scaleOffsetMin, m_scaleOffsetMax);
        }
    }
}
