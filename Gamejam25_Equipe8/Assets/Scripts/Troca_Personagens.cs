using UnityEngine;
using UnityEngine.Audio;

public class Troca_Personagens : MonoBehaviour
{
    public bool sad_player = false;
    public bool brave_player = false;
    public bool normal_player = true;

    public GameObject Sad_Object;
    public GameObject Brave_Object;
    public GameObject Normal_Object;
    public GameObject Personagens_Object;

    [Header("Sons de Transformação")]
    public AudioClip somTransformaHumano;
    public AudioClip somTransformaZumbi;
    public AudioClip somTransformaLobisomem;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SwapLobi();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwapZombie();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SwapHuman();
        }
    }

    public void SwapLobi()
    {
        sad_player = true;
        brave_player = false;
        normal_player = false;
        Update_Objects();

        PlaySound(somTransformaLobisomem);
    }

    public void SwapZombie()
    {
        sad_player = false;
        brave_player = true;
        normal_player = false;
        Update_Objects();

        PlaySound(somTransformaZumbi);
    }

    public void SwapHuman()
    {
        sad_player = false;
        brave_player = false;
        normal_player = true;
        Update_Objects();

        PlaySound(somTransformaHumano);
    }

    void Update_Objects()
    {
        Sad_Object.SetActive(sad_player);
        Brave_Object.SetActive(brave_player);
        Normal_Object.SetActive(normal_player);
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
