using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeEventSubscriptions : MonoBehaviour
{
    private int m_counter;

    void OnEnable()
    {
        if (EventManager.Instance)
        {
            EventManager.Instance.Subscribe<NodeEvents.Node24>(SpawnEnemies);
            EventManager.Instance.Subscribe<NodeEvents.Node30>(SpawnEnemies);
            EventManager.Instance.Subscribe<NodeEvents.Node24>(UpdateMachineGun);
            EventManager.Instance.Subscribe<NodeEvents.Node25>(UpdateMachineGun);
            EventManager.Instance.Subscribe<NodeEvents.Node26>(UpdateMachineGun);
            EventManager.Instance.Subscribe<NodeEvents.Node27>(UpdateMachineGun);
            EventManager.Instance.Subscribe<NodeEvents.Node28>(UpdateMachineGun);
            EventManager.Instance.Subscribe<NodeEvents.Node29>(UpdateMachineGun);
            EventManager.Instance.Subscribe<NodeEvents.Node30>(UpdateMachineGun);
            EventManager.Instance.Subscribe<NodeEvents.Node31>(UpdateMachineGun);
            EventManager.Instance.Subscribe<NodeEvents.Node32>(UpdateMachineGun);
            EventManager.Instance.Subscribe<NodeEvents.Node33>(UpdateMachineGun);
            EventManager.Instance.Subscribe<NodeEvents.Node34>(UpdateMachineGun);
            EventManager.Instance.Subscribe<NodeEvents.Node35>(UpdateMachineGun);
            EventManager.Instance.Subscribe<NodeEvents.Node36>(UpdateMachineGun);
            EventManager.Instance.Subscribe<NodeEvents.Node57>(UpdateMachineGun);
        }
    }

    void OnDisable()
    {
        if (EventManager.Instance)
        {
            EventManager.Instance.Unsubscribe<NodeEvents.Node24>(SpawnEnemies);
            EventManager.Instance.Unsubscribe<NodeEvents.Node30>(SpawnEnemies);
            EventManager.Instance.Unsubscribe<NodeEvents.Node24>(UpdateMachineGun);
            EventManager.Instance.Unsubscribe<NodeEvents.Node25>(UpdateMachineGun);
            EventManager.Instance.Unsubscribe<NodeEvents.Node26>(UpdateMachineGun);
            EventManager.Instance.Unsubscribe<NodeEvents.Node27>(UpdateMachineGun);
            EventManager.Instance.Unsubscribe<NodeEvents.Node28>(UpdateMachineGun);
            EventManager.Instance.Unsubscribe<NodeEvents.Node29>(UpdateMachineGun);
            EventManager.Instance.Unsubscribe<NodeEvents.Node30>(UpdateMachineGun);
            EventManager.Instance.Unsubscribe<NodeEvents.Node31>(UpdateMachineGun);
            EventManager.Instance.Unsubscribe<NodeEvents.Node32>(UpdateMachineGun);
            EventManager.Instance.Unsubscribe<NodeEvents.Node33>(UpdateMachineGun);
            EventManager.Instance.Unsubscribe<NodeEvents.Node34>(UpdateMachineGun);
            EventManager.Instance.Unsubscribe<NodeEvents.Node35>(UpdateMachineGun);
            EventManager.Instance.Unsubscribe<NodeEvents.Node36>(UpdateMachineGun);
            EventManager.Instance.Unsubscribe<NodeEvents.Node57>(UpdateMachineGun);
        }
    }

    void Start()
    {
        if (WeaponManager.Instance)
        {
            MachineGun machineGun = WeaponManager.Instance.GetComponentInChildren<MachineGun>();
            LaserGun laserGun = WeaponManager.Instance.GetComponentInChildren<LaserGun>();
            SingleShot singleShotGun = WeaponManager.Instance.GetComponentInChildren<SingleShot>();
            ForkGun forkGun = WeaponManager.Instance.GetComponentInChildren<ForkGun>();
            SideShot sideGun = WeaponManager.Instance.GetComponentInChildren<SideShot>();
            RadialGun radialGun = WeaponManager.Instance.GetComponentInChildren<RadialGun>();
            WeaponManager.Instance.AddWeaponEvent<NodeEvents.Node24>(laserGun);
            WeaponManager.Instance.AddWeaponEvent<NodeEvents.Node25>(singleShotGun);
            WeaponManager.Instance.AddWeaponEvent<NodeEvents.Node26>(singleShotGun);
            WeaponManager.Instance.AddWeaponEvent<NodeEvents.Node27>(radialGun);
            WeaponManager.Instance.AddWeaponEvent<NodeEvents.Node28>(sideGun);
            WeaponManager.Instance.AddWeaponEvent<NodeEvents.Node29>(singleShotGun);
            WeaponManager.Instance.AddWeaponEvent<NodeEvents.Node30>(singleShotGun);
            WeaponManager.Instance.AddWeaponEvent<NodeEvents.Node31>(forkGun);
            WeaponManager.Instance.AddWeaponEvent<NodeEvents.Node32>(singleShotGun);
            WeaponManager.Instance.AddWeaponEvent<NodeEvents.Node33>(machineGun);
            WeaponManager.Instance.AddWeaponEvent<NodeEvents.Node34>(laserGun);
            WeaponManager.Instance.AddWeaponEvent<NodeEvents.Node35>(radialGun);
            WeaponManager.Instance.AddWeaponEvent<NodeEvents.Node36>(singleShotGun);
            WeaponManager.Instance.AddWeaponEvent<NodeEvents.Node57>(singleShotGun);
        }
    }

    public void UpdateMachineGun(NodeEvents.Node24 e)
    {
        WeaponManager.Instance.UpdateWeapon<NodeEvents.Node24>(e.m_isEnabled);
    }

    public void UpdateMachineGun(NodeEvents.Node25 e)
    {
        WeaponManager.Instance.UpdateWeapon<NodeEvents.Node25>(e.m_isEnabled);
    }

    public void UpdateMachineGun(NodeEvents.Node26 e)
    {
        WeaponManager.Instance.UpdateWeapon<NodeEvents.Node26>(e.m_isEnabled);
    }

    public void UpdateMachineGun(NodeEvents.Node27 e)
    {
        WeaponManager.Instance.UpdateWeapon<NodeEvents.Node27>(e.m_isEnabled);
    }

    public void UpdateMachineGun(NodeEvents.Node28 e)
    {
        WeaponManager.Instance.UpdateWeapon<NodeEvents.Node28>(e.m_isEnabled);
    }

    public void UpdateMachineGun(NodeEvents.Node29 e)
    {
        WeaponManager.Instance.UpdateWeapon<NodeEvents.Node29>(e.m_isEnabled);
    }
    public void UpdateMachineGun(NodeEvents.Node30 e)
    {
        WeaponManager.Instance.UpdateWeapon<NodeEvents.Node30>(e.m_isEnabled);
    }
    public void UpdateMachineGun(NodeEvents.Node31 e)
    {
        WeaponManager.Instance.UpdateWeapon<NodeEvents.Node31>(e.m_isEnabled);
    }
    public void UpdateMachineGun(NodeEvents.Node32 e)
    {
        WeaponManager.Instance.UpdateWeapon<NodeEvents.Node32>(e.m_isEnabled);
    }
    public void UpdateMachineGun(NodeEvents.Node33 e)
    {
        WeaponManager.Instance.UpdateWeapon<NodeEvents.Node33>(e.m_isEnabled);
    }
    public void UpdateMachineGun(NodeEvents.Node34 e)
    {
        WeaponManager.Instance.UpdateWeapon<NodeEvents.Node34>(e.m_isEnabled);
    }
    public void UpdateMachineGun(NodeEvents.Node35 e)
    {
        WeaponManager.Instance.UpdateWeapon<NodeEvents.Node35>(e.m_isEnabled);
    }

    public void UpdateMachineGun(NodeEvents.Node36 e)
    {
        WeaponManager.Instance.UpdateWeapon<NodeEvents.Node36>(e.m_isEnabled);
    }

    public void UpdateMachineGun(NodeEvents.Node57 e)
    {
        WeaponManager.Instance.UpdateWeapon<NodeEvents.Node57>(e.m_isEnabled);
    }

    #region SpawnEnemiesHandler
    public void SpawnEnemies(NodeEvents.Node24 e)
    {
        if (e.m_isEnabled)
        {
            m_counter++;
            Debug.Log(m_counter + " DROP THE BASS");
            Spawner.Instance.SpawnRandomEnemies(3);
        }

    }

    public void SpawnEnemies(NodeEvents.Node30 e)
    {
        if (e.m_isEnabled)
        {
            m_counter++;
            if (Random.value < GameManager.Instance.m_difficultyAmplifier)
            {
                Spawner.Instance.SpawnRandomEnemies(1);
            }
        }
    }
    #endregion
}
