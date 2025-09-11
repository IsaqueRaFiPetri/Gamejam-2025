using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInScript : MonoBehaviour
{
    public Image fadeImage;      
    public float fadeDuration = 2f; 

    void Start()
    {
        StartCoroutine(FadeInEffect());
    }

    IEnumerator FadeInEffect()
    {
        Color c = fadeImage.color;
        float t = 0;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

       
        c.a = 0f;
        fadeImage.color = c;
    }
}
