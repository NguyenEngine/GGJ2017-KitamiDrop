using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageOrdering : MonoBehaviour
{
    public float zOffset = 0;
    private Transform m_transform;
    private Vector3 m_currentPos;

	void Start () 
    {
        m_transform = transform;
	}
	
	void Update () 
    {
        m_currentPos = m_transform.position;
        m_currentPos.z = m_currentPos.y + 200.0f;
        m_transform.position = m_currentPos;
	}
}
