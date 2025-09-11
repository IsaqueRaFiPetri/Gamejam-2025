using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TMP_Text))]
public class TextHoverColorEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TMP_Text tmpText;
    bool isHovering = false;
    Coroutine colorRoutine;

    [Header("Configuração de cores")]
    [SerializeField] Color normalColor = Color.green; // cor padrão
    [SerializeField] Color highlightColor = Color.red; // cor ao destacar
    [SerializeField] float letterDelay = 0.2f; // tempo entre mudar uma letra e outra

    void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    void Start()
    {
        // Garante que todo texto começa com a cor normal
        tmpText.ForceMeshUpdate();
        TMP_TextInfo textInfo = tmpText.textInfo;
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (textInfo.characterInfo[i].isVisible)
                SetCharColor(i, normalColor);
        }
        tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isHovering)
        {
            isHovering = true;
            colorRoutine = StartCoroutine(ColorCycle());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isHovering)
        {
            isHovering = false;
            if (colorRoutine != null)
                StopCoroutine(colorRoutine);

            // Volta todas as letras para cor normal
            ResetAllColors();
        }
    }

    IEnumerator ColorCycle()
    {
        TMP_TextInfo textInfo = tmpText.textInfo;

        while (isHovering)
        {
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                if (!isHovering) yield break;
                if (!textInfo.characterInfo[i].isVisible) continue;

                // Destaca letra
                SetCharColor(i, highlightColor);
                tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                yield return new WaitForSeconds(letterDelay);

                // Volta ao normal
                SetCharColor(i, normalColor);
                tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            }
        }
    }

    void SetCharColor(int index, Color c)
    {
        TMP_TextInfo textInfo = tmpText.textInfo;
        int vertexIndex = textInfo.characterInfo[index].vertexIndex;
        int meshIndex = textInfo.characterInfo[index].materialReferenceIndex;

        Color32[] vertexColors = textInfo.meshInfo[meshIndex].colors32;

        if (vertexColors.Length > 0)
        {
            vertexColors[vertexIndex + 0] = c;
            vertexColors[vertexIndex + 1] = c;
            vertexColors[vertexIndex + 2] = c;
            vertexColors[vertexIndex + 3] = c;
        }
    }

    void ResetAllColors()
    {
        TMP_TextInfo textInfo = tmpText.textInfo;
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (textInfo.characterInfo[i].isVisible)
                SetCharColor(i, normalColor);
        }
        tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }
}