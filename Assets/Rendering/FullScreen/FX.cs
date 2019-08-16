using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;
using System.Reflection;

public enum ScreenShakeType
{
    Small,
    Medium,
    Heavy
}

/// <summary>
/// In the singleton FX class are all the juice effects, for example call FX.Instance.ScreenShake(ScreenShakeType) for a screenshake
/// </summary>
public class FX : Singleton<FX> {

    public GameObject mainCamera;

    /// <summary>
	/// Call this for a screenshake effect on the main camera
	/// </summary>
	/// <param name="type">The type of screenshake</param>
    public void ScreenShake(ScreenShakeType type)
    {
        switch (type)
        {
            case ScreenShakeType.Small:
                Shake(1f, 1f);
                break;
            case ScreenShakeType.Medium:
                Shake(1f, 2f);
            break;
            case ScreenShakeType.Heavy:
                Shake(1f, 3f);
            break;
        }
    }

    private void Shake(float duration, float amplitude)
    {
        //Camera.main.transform.DORewind(false);
        //Camera.main.transform.DOShakePosition(durat7ion, amplitude);
    }

    public void PlayPostProcessEffect(GameObject prefab)
    {
        PlayPostProcessEffect(prefab, Vector2.zero, 0);
    }

    public void PlayPostProcessEffect(GameObject prefab, Vector2 center, float angle)
    {
        if (!prefab)
            return;

        BasePPFX[] PPFX = prefab.GetComponents<BasePPFX>();
        foreach (BasePPFX PPS in PPFX)
        {
            BasePPFX PPSCopy = (BasePPFX)mainCamera.AddComponent(PPS.GetType());
            CopyClassValues(PPS, PPSCopy);
            PPSCopy.SetCenter(center);
            PPSCopy.SetAngle(angle);
        }
    }

    public void CopyClassValues(BasePPFX sourceComp, BasePPFX targetComp)
    {
        FieldInfo[] sourceFields = sourceComp.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        int i = 0;
        for (i = 0; i < sourceFields.Length; i++)
        {
            var value = sourceFields[i].GetValue(sourceComp);
            sourceFields[i].SetValue(targetComp, value);
        }

        targetComp.material = Instantiate(sourceComp.material);
    }

    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ScreenShake(ScreenShakeType.Small);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ScreenShake(ScreenShakeType.Medium);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ScreenShake(ScreenShakeType.Heavy);
        }
    }*/
}
