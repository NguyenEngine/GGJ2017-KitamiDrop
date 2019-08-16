using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOverseer : Singleton<BulletOverseer> 
{
    private Transform bTransform;
    public Transform m_transform
    {
        get
        {
            if (bTransform == null)
                bTransform = transform;

            return bTransform;
        }
    }
}
