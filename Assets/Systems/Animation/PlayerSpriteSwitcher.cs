using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteSwitcher : DirectionSpriteSwitcher
{
    private PlayerInput m_playerInput;

    public override void Initialize()
    {
        m_playerInput = GetComponent<PlayerInput>();
    }

    public override void DetermineTarget()
    {
        m_target = (Vector2) m_transform.position + m_playerInput.m_lastAimInput;
    }
}
