using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public enum Emotions
{
    Normal, Sad, Brave, Medo, Happy
}
public class Troca_Personagens : MonoBehaviour
{
    public bool sad_player = false;
    public bool brave_player = false;
    public bool normal_player = true;
    public bool medo_player = false;
    public bool happy_player = false;

    public GameObject Sad_Object;
    public GameObject Brave_Object;
    public GameObject Normal_Object;
    public GameObject Medo_Object;
    public GameObject Happy_Object;
    public GameObject Personagens_Object;

    [Header("Sons de Transformação")]
    public AudioClip somTransformaHumano;
    public AudioClip somTransformaZumbi;
    public AudioClip somTransformaLobisomem;

    private AudioSource audioSource;

    PlayerInputActions playerActionsInput;

    Emotions emotions;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        playerActionsInput = new PlayerInputActions();
    }
    void OnEnable()
    {
        // Enable the action map
        playerActionsInput.Enable();

        // Subscribe to Pause action
        playerActionsInput.Player.ChangeHappy.performed += ChangeToNewEmotion;
        playerActionsInput.Player.ChangeSad.performed += ChangeToNewEmotion;
        playerActionsInput.Player.ChangeBrave.performed += ChangeToNewEmotion;
        playerActionsInput.Player.ChangeHappy.performed += ChangeToNewEmotion;
        playerActionsInput.Player.ChangeFear.performed += ChangeToNewEmotion;
    }

    void OnDisable()
    {
        // Unsubscribe to avoid leaks
        playerActionsInput.Player.ChangeHappy.performed -= ChangeToNewEmotion;
        playerActionsInput.Player.ChangeSad.performed -= ChangeToNewEmotion;
        playerActionsInput.Player.ChangeBrave.performed -= ChangeToNewEmotion;
        playerActionsInput.Player.ChangeHappy.performed -= ChangeToNewEmotion;
        playerActionsInput.Player.ChangeFear.performed -= ChangeToNewEmotion;

        playerActionsInput.Disable();
    }

    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SwapSad();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwapBrave();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SwapNormal();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            SwapMedo();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            SwapHappy();
        }
    }*/

    public void SwapSad()
    {
        sad_player = true;
        brave_player = false;
        normal_player = false;
        medo_player = false;
        happy_player = false;
        Update_Objects();

        PlaySound(somTransformaLobisomem);
    }

    public void SwapBrave()
    {
        sad_player = false;
        brave_player = true;
        normal_player = false;
        medo_player = false;
        happy_player = false;
        Update_Objects();

        PlaySound(somTransformaZumbi);
    }

    public void SwapNormal()
    {
        sad_player = false;
        brave_player = false;
        normal_player = true;
        medo_player = false;
        happy_player = false;
        Update_Objects();

        PlaySound(somTransformaHumano);
    }

    public void SwapMedo()
    {
        sad_player = false;
        brave_player = false;
        normal_player = false;
        medo_player = true;
        happy_player = false;
        Update_Objects();        
    }

    public void SwapHappy()
    {
        sad_player = false;
        brave_player = false;
        normal_player = false;
        medo_player = false;
        happy_player = true;
        Update_Objects();
    }

    void Update_Objects()
    {
        Sad_Object.SetActive(sad_player);
        Brave_Object.SetActive(brave_player);
        Normal_Object.SetActive(normal_player);
        Medo_Object.SetActive(medo_player);
        Happy_Object.SetActive(happy_player);
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    void ChangeToNewEmotion(InputAction.CallbackContext context)
    {
        switch (emotions)
        {
            case Emotions.Normal:

                break;
            case Emotions.Sad:
                break;
            case Emotions.Brave:
                break;
            case Emotions.Medo:
                break;
            case Emotions.Happy:
                break;
        }
    }
}
