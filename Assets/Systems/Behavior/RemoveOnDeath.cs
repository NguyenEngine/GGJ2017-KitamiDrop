using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOnDeath : MonoBehaviour {

    public SpriteRenderer m_aliveRenderer;
    public GameObject m_deathAnimObj;
    private LifeScript m_lifeScript;

    void Awake () {
        m_lifeScript = gameObject.GetComponent<LifeScript>();
        if (m_deathAnimObj)
            m_deathAnimObj.SetActive(false);

    }

    void OnEnable()
    {
        if (m_lifeScript != null)
            m_lifeScript.OnDeath += OnDeath;
    }

    void OnDisable()
    {
        if (m_lifeScript != null)
            m_lifeScript.OnDeath -= OnDeath;
    }
	
	void OnDeath ()
    {
        if (m_deathAnimObj)
            m_deathAnimObj.SetActive(true);
        if (m_aliveRenderer)
            m_aliveRenderer.enabled = false;
        StartCoroutine(RemoveDelayed(0.3f));
    }

    private IEnumerator RemoveDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
