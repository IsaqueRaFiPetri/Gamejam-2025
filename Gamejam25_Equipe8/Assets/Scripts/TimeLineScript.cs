using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;


public class TimeLineScript : MonoBehaviour
{
    [Header("Referências")]
    public CanvasGroup telaPreta;      
    public TextMeshProUGUI textoIntro; 
    public float fadeSpeed = 1f;
    public GameObject player;

    private void Start()
    {
        player.SetActive(false); 
        StartCoroutine(IntroSequence());
    }

    private IEnumerator IntroSequence()
    {
        // 1. Tela preta inicial
        telaPreta.alpha = 1f;
        textoIntro.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        // 2. Mostrar texto de introdução
        textoIntro.gameObject.SetActive(true);
        textoIntro.text = "Você acorda em um lugar estranho...";

        yield return new WaitForSeconds(3f);

        // 3. Fade da tela preta
        while (telaPreta.alpha > 0)
        {
            telaPreta.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }

        // 4. Espera um pouco antes de dar controle
        yield return new WaitForSeconds(1f);

        // 5. Ativa Player
        player.SetActive(true);
    }
}
