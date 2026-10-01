using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    public CanvasGroup fade;
    public float duration = 2f;

    void Start()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        fade.alpha = 1f;

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            fade.alpha = 1f - (time / duration);

            yield return null;
        }

        fade.alpha = 0f;
    }
}
