using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathFade : MonoBehaviour
{
    public Image fadeImage;

    public IEnumerator FadeToBlack(float duration)
    {
        float t = 0;
        Color c = fadeImage.color;

        while (t < duration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0, 1, t / duration);
            fadeImage.color = c;
            yield return null;
        }
    }
}
