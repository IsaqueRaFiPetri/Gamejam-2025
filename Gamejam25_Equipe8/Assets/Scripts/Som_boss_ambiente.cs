using UnityEngine;

public class Som_boss_ambiente : MonoBehaviour
{
    public AudioClip battle1;
    public AudioClip battle2;
    public AudioClip intro;
    public AudioClip theend;

    private AudioSource audiobattle1;
    private AudioSource audiobattle2;
    private AudioSource audioIntro;
    private AudioSource audioTheend;

    void Start()
    {
        audiobattle1 = gameObject.AddComponent<AudioSource>();
        audiobattle2 = gameObject.AddComponent<AudioSource>();
        audioIntro = gameObject.AddComponent<AudioSource>();
        audioTheend = gameObject.AddComponent<AudioSource>();

        audiobattle1.clip = battle1;
        audiobattle1.loop = true;
        audiobattle1.volume = 0.001f;
        audiobattle1.Play();

        audiobattle2.clip = battle2;
        audiobattle2.loop = true;
        audiobattle2.volume = 0.25f;
        audiobattle2.Play();

        audioIntro.clip = intro;
        audioIntro.loop = true;
        audioIntro.volume = 0.025f;
        audioIntro.Play();

        audioTheend.clip = theend;
        audioTheend.loop = true;
        audioTheend.volume = 0.8f;
        audioTheend.Play();
    }

}
