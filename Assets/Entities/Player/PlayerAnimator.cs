using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    // For velocity
    private PlayerInput m_playerInput;
    private LifeScript m_playerLife;
    private Transform m_transform;
    private Vector2 m_target;
    private Rigidbody2D m_rb2d;
    
    public GameObject m_bodyObjectUp;
    public GameObject m_bodyObjectRight;
    public GameObject m_bodyObjectDown;
    public GameObject m_bodyObjectSurrender;
    public float m_leftBodyOffsetX;
    public float m_rightBodyOffsetX;

    public GameObject m_gunObjectUp;
    public GameObject m_gunObjectRight;
    public GameObject m_gunObjectDown;
    public float m_leftGunOffsetX;
    public float m_rightGunOffsetX;

    private List<Animator> m_gunAnimators = new List<Animator>();
    private List<SpriteRenderer> m_gunRenderers = new List<SpriteRenderer>();
    private List<Sprite> m_gunDefaultSprites = new List<Sprite>();

    private bool m_alive;
    private bool m_lookingRight;
    private bool m_movingUp;

    void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
        m_playerLife = GetComponent<LifeScript>();
        m_transform = transform;
        m_rb2d = GetComponent<Rigidbody2D>();

        m_alive = m_playerLife.IsAlive();
        m_playerLife.OnDeath += OnPlayerDeath;

        GameObject[] guns = { m_gunObjectUp, m_gunObjectRight, m_gunObjectDown };
        foreach (GameObject go in guns)
        {
            Animator gunAnim = go.GetComponent<Animator>();
            m_gunAnimators.Add(gunAnim);
            gunAnim.enabled = false;

            SpriteRenderer renderer = go.GetComponent<SpriteRenderer>();
            m_gunRenderers.Add(renderer);
            m_gunDefaultSprites.Add(renderer.sprite);
        }

        HideBodyObjects();
        HideGunObjects();
    }

    void Update()
    {
        m_target = (Vector2)m_transform.position + m_playerInput.m_lastAimInput;
        m_lookingRight = (m_target.x - m_transform.position.x) > 0;
        m_movingUp = (m_rb2d.velocity.y > 0);

        if (m_alive)
        {
            SwitchBodyObjects();
            SwitchGunObjects();
        }

        //if (Input.GetMouseButtonDown(0))
        //    FireGun();
    }

    private void OnPlayerDeath()
    {
        m_alive = false;
        HideBodyObjects();
        HideGunObjects();
        m_bodyObjectSurrender.SetActive(true);

        Collider2D anyCol = GetComponent<Collider2D>();
        m_rb2d.velocity = Vector2.zero;
        anyCol.enabled = false;

        m_bodyObjectSurrender.transform.localPosition = new Vector3(m_lookingRight ? m_rightBodyOffsetX : m_leftBodyOffsetX, 0, 0);
        m_bodyObjectSurrender.transform.localScale = new Vector3(m_lookingRight ? 1.0f : -1.0f, 1, 1);
    }

    public void FireGun()
    {
        foreach (Animator anim in m_gunAnimators)
        {
            anim.enabled = true;
            anim.Play(0);
        }
    }

    private void HideBodyObjects()
    {
        GameObject[] allObjects = { m_bodyObjectUp, m_bodyObjectRight, m_bodyObjectDown, m_bodyObjectSurrender };
        foreach (GameObject sideObject in allObjects)
            if (sideObject)
                sideObject.SetActive(false);
    }

    private void HideGunObjects()
    {
        GameObject[] allObjects = { m_gunObjectUp, m_gunObjectRight, m_gunObjectDown };
        foreach (GameObject sideObject in allObjects)
            if (sideObject)
                sideObject.SetActive(false);

        for (int i = 0; i < m_gunAnimators.Count; ++i)
        {
            m_gunAnimators[i].enabled = false;
            m_gunRenderers[i].sprite = m_gunDefaultSprites[i];
        }
    }

    private void SwitchBodyObjects()
    {
        float angleRad = Mathf.Atan2(m_rb2d.velocity.y, m_rb2d.velocity.x);
        float angleInDegrees = Mathf.Rad2Deg * angleRad + 180;

        GameObject targetObject = m_bodyObjectRight;
        if (angleInDegrees < 45 || angleInDegrees >= 315 || angleInDegrees >= 135 && angleInDegrees < 225)
        {
            targetObject = m_bodyObjectRight;
        }
        else if (angleInDegrees >= 45 && angleInDegrees < 135)
        {
            targetObject = m_bodyObjectDown;
        }
        else if (angleInDegrees >= 225 && angleInDegrees < 315)
        {
            targetObject = m_bodyObjectUp;
        }

        if (targetObject && !targetObject.activeSelf)
        {
            HideBodyObjects();
            if (targetObject)
                targetObject.SetActive(true);
        }

        if (targetObject)
        {
            targetObject.transform.localPosition = new Vector3(m_lookingRight ? m_rightBodyOffsetX : m_leftBodyOffsetX, 0, 0);
            targetObject.transform.localScale = new Vector3(m_lookingRight ? 1.0f : -1.0f, 1, 1);
        }
    }

    private void SwitchGunObjects()
    {
        float angleRad = Mathf.Atan2(m_target.y - m_transform.position.y, m_target.x - m_transform.position.x);
        float angleInDegrees = Mathf.Rad2Deg * angleRad + 180;

        GameObject targetObject = m_gunObjectRight;
        if (angleInDegrees < 45 || angleInDegrees >= 315 || angleInDegrees >= 135 && angleInDegrees < 225)
        {
            targetObject = m_gunObjectRight;
        }
        else if (angleInDegrees >= 45 && angleInDegrees < 135)
        {
            targetObject = m_gunObjectDown;
        }
        else if (angleInDegrees >= 225 && angleInDegrees < 315)
        {
            targetObject = m_gunObjectUp;
        }

        if (targetObject && !targetObject.activeSelf)
        {
            HideGunObjects();
            if (targetObject)
                targetObject.SetActive(true);
        }

        if (targetObject)
        {
            targetObject.transform.localPosition = new Vector3(m_lookingRight ? m_rightGunOffsetX : m_leftGunOffsetX, 0, -1);
            targetObject.transform.localScale = new Vector3(m_lookingRight ? 1.0f : -1.0f, 1, 1);
        }
    }


}
