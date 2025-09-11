using UnityEngine;

public class Som_boss_ambiente : MonoBehaviour
{
    public AudioClip battle1;
    public AudioClip battle2;

    private AudioSource audiobattle1;
    private AudioSource audiobattle2;
    
    void Start()
    {
        audiobattle1 = gameObject.AddComponent<AudioSource>();
        audiobattle2 = gameObject.AddComponent<AudioSource>();

        audiobattle1.clip = battle1;
        audiobattle1.loop = true;
        audiobattle1.volume = 0.05f;
        audiobattle1.Play();

        audiobattle2.clip = battle2;
        audiobattle2.loop = true;
        audiobattle2.volume = 0.5f;
        audiobattle2.Play();
    }

}
