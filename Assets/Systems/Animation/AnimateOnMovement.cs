using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateOnMovement : MonoBehaviour {

    public Rigidbody2D rb2d;
    private Animator m_animator;
    
	void Awake () {
        m_animator = gameObject.GetComponent<Animator>();
        if (!m_animator || !rb2d)
            DestroyImmediate(this);
    }
	
	void Update () {
        m_animator.enabled = rb2d.velocity.SqrMagnitude() > 0.2 * 0.2;
    }
}
