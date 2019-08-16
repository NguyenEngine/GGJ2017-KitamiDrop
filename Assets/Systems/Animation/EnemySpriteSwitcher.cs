using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteSwitcher : DirectionSpriteSwitcher
{
    public override void DetermineTarget()
    {
        // Get closest player later.
        m_target = PlayerManager.Instance.m_player1.m_transform.position;
    }
}
