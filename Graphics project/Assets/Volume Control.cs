using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeControl : MonoBehaviour
{
    public Volume targetVolume;
    [Range(0f, 1f)] public float startWeight = 0f;
    [Range(0f, 1f)] public float endWeight = 1f;
    [Range(0.01f, 30f)] public float duration = 1f;

    float currentWeight;
    Coroutine transitionCoroutine;

    void Start()
    {
        if (targetVolume != null)
        {
            currentWeight = startWeight;
            targetVolume.weight = startWeight;
        }
    }

    private void OnEnable()
    {
        TransitionVolume();
    }

    public void TransitionVolume()
    {
        if (targetVolume == null)
        {
            Debug.LogWarning("VolumeControl targetVolume is not assigned.");
            return;
        }

        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);

        transitionCoroutine = StartCoroutine(DoTransition());
    }

    IEnumerator DoTransition()
    {
        float elapsed = 0f;
        float start = currentWeight;
        float end = endWeight;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            currentWeight = Mathf.Lerp(start, end, t);
            targetVolume.weight = currentWeight;
            yield return null;
        }

        currentWeight = end;
        targetVolume.weight = end;
        transitionCoroutine = null;
    }
}


