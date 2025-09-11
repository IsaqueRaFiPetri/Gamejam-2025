using UnityEngine;

public class Som_boss_ambiente : MonoBehaviour
{
    public AudioClip battle1;    
    public AudioClip intro;
    public AudioClip theend;

    private AudioSource audiobattle1;   
    private AudioSource audioIntro;
    private AudioSource audioTheend;

    void Start()
    {
        audiobattle1 = gameObject.AddComponent<AudioSource>();        
        audioIntro = gameObject.AddComponent<AudioSource>();
        audioTheend = gameObject.AddComponent<AudioSource>();

        audiobattle1.clip = battle1;
        audiobattle1.loop = true;
        audiobattle1.volume = 0.1f;
        audiobattle1.Play();
       
        audioIntro.clip = intro;
        audioIntro.loop = true;
        audioIntro.volume = 20f;
        audioIntro.Play();

        audioTheend.clip = theend;
        audioTheend.loop = true;
        audioTheend.volume = 3f;
        audioTheend.Play();
    }

}
