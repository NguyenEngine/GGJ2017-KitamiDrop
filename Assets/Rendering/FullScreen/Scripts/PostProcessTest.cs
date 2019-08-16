using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessTest : MonoBehaviour {

    public GameObject[] prefabs;

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetMouseButtonDown(0))
            ShowPrefab(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ShowPrefab(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ShowPrefab(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            ShowPrefab(3);

    }

    void ShowPrefab(int index)
    {
        if (index >= 0 && index < prefabs.Length && prefabs[index] != null)
        {
            Vector2 mousePos = Input.mousePosition;
            FX.Instance.PlayPostProcessEffect(prefabs[index], mousePos, 0);
        }
    }
}
