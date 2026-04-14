using System.Collections;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public Light targetLight;
    public float startIntensity = 0f;
    public float endIntensity = 1f;
    [Range(0.01f, 30f)] public float duration = 1f;

    float currentIntensity;
    Coroutine transitionCoroutine;

    void Start()
    {
        if (targetLight != null)
        {
            currentIntensity = startIntensity;
            targetLight.intensity = startIntensity;
        }
    }

    private void OnEnable()
    {
        TransitionLightIntensity();
    }

    public void TransitionLightIntensity()
    {
        if (targetLight == null)
        {
            Debug.LogWarning("LightControl targetLight is not assigned.");
            return;
        }

        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);

        transitionCoroutine = StartCoroutine(DoTransition());
    }

    IEnumerator DoTransition()
    {
        float elapsed = 0f;
        float start = currentIntensity;
        float end = endIntensity;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            currentIntensity = Mathf.Lerp(start, end, t);
            targetLight.intensity = currentIntensity;
            yield return null;
        }

        currentIntensity = end;
        targetLight.intensity = end;
        transitionCoroutine = null;
    }
}


