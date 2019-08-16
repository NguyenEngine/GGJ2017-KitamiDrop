using UnityEngine;
using System.Collections;

public class PlayerInput : PlayerRoot
{
    public bool m_inputEnabled = true;
    public bool m_useController = false;

    public Vector2 m_movementInput,
                   m_aimInput,
                   m_lastAimInput;

    // Xbox Controller Keys
    [HideInInspector]
    public string  m_trigger = "",
                   m_leftStickX = "",
                   m_leftStickY = "",
                   m_rightStickX = "",
                   m_rightStickY = "",
                   m_leftBumper = "",
                   m_rightBumper = "";                


    public void InitializeInput()
    {
        InputHandler.Instance.SetInputStrings(this);
    }

    void Start()
    {
        m_lastAimInput = Vector2.right;
    }

    void Update()
    {
        if (m_inputEnabled)
        {
            UpdateInputMode();
            UpdateMovementInput();
        }
    }

    private void UpdateInputMode()
    {
        if (!m_inputEnabled)
            return;
        if (m_useController && (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0))
        {
            m_useController = false;
            m_movementInput = Vector2.zero;
            m_lastAimInput = Vector2.right;
            Debug.Log("Switching to mouse");
        }
        if (!m_useController && (Input.GetAxis(m_leftStickX) != 0 || Input.GetAxis(m_leftStickY) != 0 || Input.GetAxis(m_rightStickX) != 0 || Input.GetAxis(m_rightStickY) != 0))
        {
            m_useController = true;
            m_movementInput = Vector2.zero;
            m_lastAimInput = Vector2.right;
            Debug.Log("Switching to controller");
        }
    }

    private void UpdateMovementInput()
    {
        m_movementInput = Vector2.zero;
        m_aimInput = Vector2.zero;

        if (m_useController)
        {
            m_movementInput.x = Input.GetAxis(m_leftStickX);
            m_movementInput.y = Input.GetAxis(m_leftStickY);
            m_aimInput.x = Input.GetAxis(m_rightStickX);
            m_aimInput.y = Input.GetAxis(m_rightStickY);
        }
        else
        {
            m_movementInput.x = Input.GetAxis("Horizontal");
            m_movementInput.y = Input.GetAxis("Vertical");
            m_aimInput = (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_mainCamera.position.z)) - m_transform.position).normalized;
        }

        if (m_movementInput != Vector2.zero)
        {
            m_movementInput.Normalize();
        }

        if (m_aimInput != Vector2.zero) 
        {
            m_aimInput.Normalize();
            m_lastAimInput = m_aimInput;
        }
    }
}
