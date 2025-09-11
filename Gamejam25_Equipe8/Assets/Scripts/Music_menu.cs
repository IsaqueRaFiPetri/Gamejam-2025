using UnityEngine;

public class Music_menu : MonoBehaviour
{    
    public AudioClip cave_crystal;
    private AudioSource audiocave_crystal;
    public AudioClip cyberpunk;
    private AudioSource audiocyberpunk;
    public AudioClip lurid;
    private AudioSource audiolurid;
    public AudioClip song18;
    private AudioSource audiosong18d;
    public AudioClip xingling;
    private AudioSource audioxingling;

    void Start()
    {      
        audiocave_crystal = gameObject.AddComponent<AudioSource>();
        audiocyberpunk = gameObject.AddComponent<AudioSource>();
        audiolurid = gameObject.AddComponent<AudioSource>();
        audiosong18d = gameObject.AddComponent<AudioSource>();
        audioxingling = gameObject.AddComponent<AudioSource>();       

        audiocave_crystal.clip = cave_crystal;
        audiocave_crystal.loop = true;
        audiocave_crystal.volume = 4f;
        audiocave_crystal.Play();

        audiocyberpunk.clip = cyberpunk;
        audiocyberpunk.loop = true;
        audiocyberpunk.volume = 0.1f;
        audiocyberpunk.Play();

        audiolurid.clip = lurid;
        audiolurid.loop = true;
        audiolurid.volume = 4.2f;
        audiolurid.Play();

        audiosong18d.clip = song18;
        audiosong18d.loop = true;
        audiosong18d.volume = 5f;
        audiosong18d.Play();

        audioxingling.clip = xingling;
        audioxingling.loop = true;
        audioxingling.volume = 5f;
        audioxingling.Play();


    }
    
}
