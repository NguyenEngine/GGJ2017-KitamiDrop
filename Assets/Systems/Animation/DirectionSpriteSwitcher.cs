using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DirectionSpriteSwitcher : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite m_spriteUp;
    public Sprite m_spriteLeft;
    public Sprite m_spriteDown;
    public Sprite m_spriteRight;

    protected Transform m_transform;
    protected Vector3 m_target;
    private SpriteRenderer m_spriteRenderer;
    
    void Start()
    {
        m_transform = transform;
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        Initialize();
    }

    void Update()
    {
        DetermineTarget();
        SwitchSprite();
    }

    public virtual void Initialize() { }

    public virtual void DetermineTarget()
    {
        throw new NotImplementedException("SwitchSpriteCaller was not implemented by child class.");
    }

    public void SwitchSprite()
    {
        float angleRad = Mathf.Atan2(m_target.y - m_transform.position.y, m_target.x - m_transform.position.x);
        float angleInDegrees = Mathf.Rad2Deg * angleRad + 180;

        Sprite targetSprite = m_spriteRight;
        if (angleInDegrees < 45 || angleInDegrees >= 315)
        {
            targetSprite = m_spriteLeft;
        }
        else if (angleInDegrees >= 45 && angleInDegrees < 135)
        {
            targetSprite = m_spriteDown;
        }
        else if (angleInDegrees >= 135 && angleInDegrees < 225)
        {
            targetSprite = m_spriteRight;
        }
        else if (angleInDegrees >= 225 && angleInDegrees < 315)
        {
            targetSprite = m_spriteUp;
        }
        m_spriteRenderer.sprite = targetSprite;
    }
}