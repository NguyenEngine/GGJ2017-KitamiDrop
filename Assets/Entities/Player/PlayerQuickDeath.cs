using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuickDeath : MonoBehaviour 
{
    void OnEnable()
    {
        GetComponent<LifeScript>().OnDeath += QuickDeath;
    }

    void OnDisable()
    {
        GetComponent<LifeScript>().OnDeath -= QuickDeath;
    }

    void QuickDeath()
    {
        Debug.Log("Test");
        GameManager.Instance.GameOver();
    }
}
