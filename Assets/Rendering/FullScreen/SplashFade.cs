using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashFade : MonoBehaviour {

    public float duration = 3;
    public SpriteRenderer renderer;

	void Update () {
		if (renderer)
        {
            float intensity = Mathf.Clamp(duration, 0, 1);
            renderer.color = new Color(1, 1, 1, intensity);
        }

        duration = duration - Time.deltaTime;
        if (duration < 0)
            Application.LoadLevel("MainLevel_Lex2");
	}
}
