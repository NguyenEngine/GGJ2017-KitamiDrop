using UnityEngine;
using System.Collections;

public class PlayerBase : PlayerRoot 
{
    public int m_playerId = 1;
    public bool m_isEnemy = false;

    void Start()
    {
        if (m_isEnemy)
        {
            m_playerInput.m_inputEnabled = false;
        }
        else
        {
            m_playerInput.InitializeInput();
        }
    }
}
