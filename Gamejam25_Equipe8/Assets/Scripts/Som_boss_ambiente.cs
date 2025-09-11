using UnityEngine;

public class Som_boss_ambiente : MonoBehaviour
{
    public AudioClip battle1;
    public AudioClip epic;
    public AudioClip intro;
    public AudioClip theend;

    private AudioSource audiobattle1;
    private AudioSource audioEpic;
    private AudioSource audioIntro;
    private AudioSource audioTheend;

    void Start()
    {
        audiobattle1 = gameObject.AddComponent<AudioSource>();
        audioEpic = gameObject.AddComponent<AudioSource>();
        audioIntro = gameObject.AddComponent<AudioSource>();
        audioTheend = gameObject.AddComponent<AudioSource>();

        audiobattle1.clip = battle1;
        audiobattle1.loop = true;
        audiobattle1.volume = 0.001f;
        audiobattle1.Play();

        audioEpic.clip = epic;
        audioEpic.loop = true;
        audioEpic.volume = 0.65f;
        audioEpic.Play();

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
