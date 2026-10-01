using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour
{
    public CanvasGroup fade;
    public float duration = 2f;

    public void _Start()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        fade.alpha = 0f;
        
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            fade.alpha = time / duration;

            yield return null;
        }

        fade.alpha = 1f;
    }
}