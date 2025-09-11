using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    [TextArea] public List<string> texts;
    public float delayPerCharacter = 0.05f;
    public string nextSceneName;

    [Header("Fade Config")]
    public Image fadeImage; // Imagem preta em tela cheia
    public float fadeDuration = 1.5f; // tempo do fade

    private Coroutine typingCoroutine;
    private int currentIndex = 0;
    private void Start()
    {
        if (fadeImage != null)
        {    
            Color color = fadeImage.color;
            color.a = 1f;
            fadeImage.color = color;
            fadeImage.gameObject.SetActive(true);
            StartCoroutine(FadeIn());
        }
        else
        {
            BeginTextSequence();
        }
    }
    private void OnDisable()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
    }
    IEnumerator FadeIn()
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(1f - (elapsed / fadeDuration)); // de 1 até 0
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        fadeImage.gameObject.SetActive(false); 
        BeginTextSequence();
    }
    void BeginTextSequence()
    {
        if (textMeshPro != null && texts.Count > 0)
        {
            typingCoroutine = StartCoroutine(ShowTextGradually(texts[currentIndex]));
        }
    }

    IEnumerator ShowTextGradually(string fullText)
    {
        textMeshPro.text = "";
        for (int i = 0; i <= fullText.Length; i++)
        {
            textMeshPro.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(delayPerCharacter);
        }
        yield return new WaitForSeconds(0.5f); 
        currentIndex++;
        if (currentIndex < texts.Count)
        {
            typingCoroutine = StartCoroutine(ShowTextGradually(texts[currentIndex]));
        }
        else
        {
            StartCoroutine(FadeOutAndChangeScene());
        }
    }

    IEnumerator FadeOutAndChangeScene()
    {
        fadeImage.gameObject.SetActive(true);

        float elapsed = 0f;
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration); 
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(2f); 
        SceneManager.LoadScene(nextSceneName);
    }
}
