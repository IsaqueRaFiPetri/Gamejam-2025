using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum Emotions
{
    Normal, Sad, Brave, Fear, Happy
}
public class Troca_Personagens : MonoBehaviour
{
    public static Troca_Personagens instance;
    [HideInInspector] public bool isNormal = true, isSad, isBrave, isHappy, isFear;

    [Header("Objeto Modos")]
    public GameObject Sad_Object, Brave_Object, Normal_Object, Medo_Object, Happy_Object, Personagens_Object;

    [Header("Imagem Modos")]
    [SerializeField] Image normal, sad, brave, fear, happy;
     
    [Header("Sons de Transformação")]
    [SerializeField] AudioClip transformationSound;

    AudioSource audioSource;

    PlayerInputActions playerActionsInput;

    Emotions emotions;

    private void Awake()
    {
        playerActionsInput = new PlayerInputActions();
        instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    void OnEnable()
    {
        playerActionsInput.Enable();

        playerActionsInput.Player.ChangeNormal.performed += ctx => Swap(Emotions.Normal);
        playerActionsInput.Player.ChangeSad.performed += ctx => Swap(Emotions.Sad);
        playerActionsInput.Player.ChangeBrave.performed += ctx => Swap(Emotions.Brave);
        playerActionsInput.Player.ChangeFear.performed += ctx => Swap(Emotions.Fear);
        playerActionsInput.Player.ChangeHappy.performed += ctx => Swap(Emotions.Happy);
    }

    public void Swap(Emotions newEmotion)
    {
        isSad = isBrave = isNormal = isFear = isHappy = false;

        emotions = newEmotion;

        switch (newEmotion)
        {
            case Emotions.Normal:
                isNormal = true;
                PlaySound(transformationSound);
                break;
            case Emotions.Sad:
                isSad = true;
                PlaySound(transformationSound);
                break;
            case Emotions.Brave:
                isBrave = true;
                PlaySound(transformationSound);
                break;
            case Emotions.Fear:
                isFear = true;
                PlaySound(transformationSound);
                break;
            case Emotions.Happy:
                isHappy = true;
                PlaySound(transformationSound);
                break;
        }
        Update_Objects();
    }


    void Update_Objects()
    {
        Sad_Object.SetActive(isSad);
        Brave_Object.SetActive(isBrave);
        Normal_Object.SetActive(isNormal);
        Medo_Object.SetActive(isFear);
        Happy_Object.SetActive(isHappy);
        sad.gameObject.SetActive(isSad);
        brave.gameObject.SetActive(isBrave);
        normal.gameObject.SetActive(isNormal);
        fear.gameObject.SetActive(isFear);
        happy.gameObject.SetActive(isHappy);
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}