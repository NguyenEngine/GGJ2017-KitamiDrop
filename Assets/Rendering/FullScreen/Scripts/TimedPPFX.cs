using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedPPFX : MonoBehaviour {

    public GameObject glitchPPFX;
    public GameObject ripplePPFX;
    
    void OnEnable()
    {
        if (EventManager.Instance)
        {
            EventManager.Instance.Subscribe<NodeEvents.Node24>(HandleNodeEvent24);
            EventManager.Instance.Subscribe<NodeEvents.Node36>(HandleNodeEvent36);
        }
    }

    void OnDisable()
    {
        if (EventManager.Instance)
        {
            EventManager.Instance.Unsubscribe<NodeEvents.Node24>(HandleNodeEvent24);
            EventManager.Instance.Unsubscribe<NodeEvents.Node36>(HandleNodeEvent36);
        }
    }

    private void HandleNodeEvent24(NodeEvents.Node24 Event)
    {
        if (Event.m_isEnabled)
            FX.Instance.PlayPostProcessEffect(glitchPPFX);
    }

    private void HandleNodeEvent36(NodeEvents.Node36 Event)
    {
        if (Event.m_isEnabled)
        {
            Vector3 randomScreenPos = Camera.main.ViewportToScreenPoint(new Vector3(Random.value, Random.value, Camera.main.transform.position.z));
            FX.Instance.PlayPostProcessEffect(ripplePPFX, randomScreenPos, 0);
        }
    }
}
