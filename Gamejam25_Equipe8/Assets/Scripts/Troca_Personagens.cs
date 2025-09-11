using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum Emotions
{
    Normal, Sad, Brave, Fear, Happy
}

public class Troca_Personagens : MonoBehaviour
{
    public static Troca_Personagens instance;

    [HideInInspector] public bool isNormal = true, isSad, isBrave, isHappy, isFear;

    [SerializeField] float transformationTime;

    [Header("Objetos de Personagem")]
    public GameObject Sad_Object, Brave_Object, Normal_Object, Fear_Object, Happy_Object;

    [Header("Imagens UI")]
    [SerializeField] Image normal, sad, brave, fear, happy;

    [Header("Som de Transformação")]
    [SerializeField] AudioClip transformationSound;

    AudioSource audioSource;

    private bool fearTriggered = false;
    private bool isTemporaryEmotion = false;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Swap(Emotions newEmotion, bool temporary = false)
    {
        if (isTemporaryEmotion && !temporary)
            return; // Não sobrescreve uma emoção temporária

        ResetEmotions();

        switch (newEmotion)
        {
            case Emotions.Normal: isNormal = true; break;
            case Emotions.Sad: isSad = true; break;
            case Emotions.Brave:
                isBrave = true;
                if (temporary) StartCoroutine(SwapBack(transformationTime));
                break;
            case Emotions.Fear:
                isFear = true;
                if (temporary) StartCoroutine(SwapBack(transformationTime));
                break;
            case Emotions.Happy: isHappy = true; break;
        }

        isTemporaryEmotion = temporary;

        PlayerStatus.instance.CollectEmotion(newEmotion);

        PlaySound(transformationSound);
        UpdateObjects();
    }

    void ResetEmotions()
    {
        isNormal = isSad = isBrave = isFear = isHappy = false;
    }

    void UpdateObjects()
    {
        Sad_Object.SetActive(isSad);
        Brave_Object.SetActive(isBrave);
        Normal_Object.SetActive(isNormal);
        Fear_Object.SetActive(isFear);
        Happy_Object.SetActive(isHappy);

        sad.gameObject.SetActive(isSad);
        brave.gameObject.SetActive(isBrave);
        normal.gameObject.SetActive(isNormal);
        fear.gameObject.SetActive(isFear);
        happy.gameObject.SetActive(isHappy);

        // Atualiza referência no movimento
        PlayerMovTopdown playerMov = GetComponent<PlayerMovTopdown>();
        if (playerMov != null) playerMov.UpdateAnimator();
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }

    IEnumerator SwapBack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isTemporaryEmotion = false;
        Swap(Emotions.Normal);
    }

    // ---------------- Gatilhos específicos ---------------- //

    public void ActivateHappy()
    {
        if (PlayerStatus.instance.playerStatus.emotion >= 75f || EnemyKilled())
            Swap(Emotions.Happy);
    }

    public void ActivateSad()
    {
        Swap(Emotions.Sad);
    }

    public void ActivateBrave()
    {
        Swap(Emotions.Brave, true);
    }

    public void ActivateFear()
    {
        if (!fearTriggered && EnemyDetected())
        {
            fearTriggered = true;
            Swap(Emotions.Fear, true);
        }
    }

    bool EnemyKilled()
    {
        return FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length == 0;
    }

    bool EnemyDetected()
    {
        Enemy enemy = FindFirstObjectByType<Enemy>();
        if (enemy == null) return false;
        float distance = Vector2.Distance(transform.position, enemy.transform.position);
        return distance < 5f; // ajuste a distância de detecção
    }
}
