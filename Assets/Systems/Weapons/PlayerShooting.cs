using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShooting : PlayerRoot
{
    public bool m_isShooting = true;
    public float m_fireRate = 10f;
    public GameObject m_tempPrefab;
    public float m_distanceOffset = 1f;
    private float m_delayTimerMax;
    private float m_delayTimerCurrent;

    public GameObject m_bulletSceneParent;
    public PlayerAnimator m_playerAnimator;

    private LifeScript m_playerLife;

    void Awake()
    {
        m_playerLife = GetComponent<LifeScript>();
    }

    void Update()
    {
        if (m_isShooting)
        {
            m_delayTimerMax = 1.0f / m_fireRate;
            m_delayTimerCurrent += Time.deltaTime;

            if (m_delayTimerCurrent >= m_delayTimerMax)
            {
                m_delayTimerCurrent = 0.0f;
                PlayerShoot();
            }
        }
    }

    private void PlayerShoot()
    {
        if (m_playerInput && m_tempPrefab)
        {
            if (WeaponManager.Instance.m_activeWeapons.Count > 0)
            {
                Shoot(WeaponManager.Instance.m_activeWeapons[0].m_bulletPrefab, m_playerInput.m_lastAimInput);
            }
            else
            {
                Shoot(m_tempPrefab, m_playerInput.m_lastAimInput);
            }
        }
    }

    public Bullet Shoot(GameObject bulletPrefab, Vector2 direction)
    {
        if (m_playerLife && !m_playerLife.IsAlive())
            return null;

        GameObject bullet = (GameObject)Instantiate(bulletPrefab, (Vector2)m_transform.position + m_distanceOffset * direction, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.m_targetDirection = direction;
        if (BulletOverseer.Instance) 
        {
            bullet.transform.SetParent(BulletOverseer.Instance.m_transform);
        }

        if (m_bulletSceneParent)
            bullet.transform.parent = m_bulletSceneParent.transform;

        if (m_playerAnimator)
            m_playerAnimator.FireGun();

        return bulletScript;
    }

    void OnEnable()
    {
        if (EventManager.Instance)
        {
            EventManager.Instance.Subscribe<NodeEvents.Node24>(TestMethod);
            EventManager.Instance.Subscribe<NodeEvents.Node25>(TestMethod);
            EventManager.Instance.Subscribe<NodeEvents.Node26>(TestMethod);
            EventManager.Instance.Subscribe<NodeEvents.Node27>(TestMethod);
        }
    }
    void OnDisable()
    {
        if (EventManager.Instance)
        {
            EventManager.Instance.Unsubscribe<NodeEvents.Node24>(TestMethod);
            EventManager.Instance.Unsubscribe<NodeEvents.Node25>(TestMethod);
            EventManager.Instance.Unsubscribe<NodeEvents.Node26>(TestMethod);
            EventManager.Instance.Unsubscribe<NodeEvents.Node27>(TestMethod);
        }

    }
    public void TestMethod(NodeEvents.Node24 e)
    {
        if (e.m_isEnabled)
        {
            PlayerShoot();
        }

    }
    public void TestMethod(NodeEvents.Node25 e)
    {
        if (e.m_isEnabled)
        {
            PlayerShoot();
        }

    }
    public void TestMethod(NodeEvents.Node26 e)
    {
        if (e.m_isEnabled)
        {
            PlayerShoot();
        }

    }
    public void TestMethod(NodeEvents.Node27 e)
    {
        if (e.m_isEnabled)
        {
            PlayerShoot();
        }
    }
}
