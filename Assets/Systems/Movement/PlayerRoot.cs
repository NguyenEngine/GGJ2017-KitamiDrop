using UnityEngine;
using System.Collections;

public class PlayerRoot : MonoBehaviour
{
    private Rigidbody2D pRigidbody2D;
    public Rigidbody2D m_rigidbody2D
    {
        get
        {
            if (pRigidbody2D == null)
                pRigidbody2D = GetComponent<Rigidbody2D>();

            return pRigidbody2D;
        }
    }

    private Transform pTransform;
    public Transform m_transform
    {
        get
        {
            if (pTransform == null)
                pTransform = transform;

            return pTransform;
        }
    }

    private Transform pCameraTransform;
    public Transform m_mainCamera
    {
        get
        {
            if (pCameraTransform == null)
                pCameraTransform = Camera.main.transform;

            return pCameraTransform;
        }
    }

    private PlayerMovement2D pMovment2D;
    public PlayerMovement2D m_playerMovement2D
    {
        get
        {
            if (pMovment2D == null)
                pMovment2D = GetComponent<PlayerMovement2D>();

            return pMovment2D;
        }
    }

    private PlayerInput pInput;
    public PlayerInput m_playerInput
    {
        get
        {
            if (pInput == null)
                pInput = GetComponent<PlayerInput>();

            return pInput;
        }
    }

    private PlayerBase pBase;
    public PlayerBase m_playerBase
    {
        get
        {
            if (pBase == null)
                pBase = GetComponent<PlayerBase>();

            return pBase;
        }
    }

    private PlayerShooting pShooting;
    public PlayerShooting m_playerShooting
    {
        get
        {
            if (pShooting == null)
                pShooting = GetComponent<PlayerShooting>();

            return pShooting;
        }
    }
}
