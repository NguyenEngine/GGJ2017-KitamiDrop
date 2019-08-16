using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBulletHitsEnemyEvent : EventBase 
{
    public Vector3 m_position;

    public OnBulletHitsEnemyEvent(Vector3 position)
    {
        m_position = position;
    }
}
