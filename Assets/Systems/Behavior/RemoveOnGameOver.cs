using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOnGameOver : MonoBehaviour {

    public float Delay = 0;
    
    void OnEnable()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.OnGameOver += OnGameOver;
        }
    }

    void OnDisable()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.OnGameOver -= OnGameOver;
        }
    }

    void OnGameOver()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(Delay);
        Destroy(gameObject);
    }
}
