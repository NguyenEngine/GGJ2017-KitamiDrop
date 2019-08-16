using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePPFX : MonoBehaviour {
    // Material that applies the post process shader
    public Material material;
    // Duration to play this script, or -1 for non-expiring
    public float duration = -1.0f;

    void Start()
    {
        if (duration > 0)
        {
            StartCoroutine(ScheduleDestruction());
        }

        material.SetFloat("_EffectT", Time.time);
    }

    public void SetCenter(Vector2 center)
    {
        material.SetFloat("_EffectX", center.x);
        material.SetFloat("_EffectY", center.y);
    }

    public void SetAngle(float angle)
    {
        material.SetFloat("_EffectA", angle);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, material);
    }

    IEnumerator ScheduleDestruction()
    {
        yield return new WaitForSeconds(duration);
        Destroy(this);
    }
}
