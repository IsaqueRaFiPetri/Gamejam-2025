using UnityEngine;

public class Music_menu : MonoBehaviour
{
    public AudioClip lava_city;
    private AudioSource audioShip;
    public AudioClip ship;
    private AudioSource audiolava_city;
    public AudioClip underground_cave;
    private AudioSource audioUnderground_cave;
    public AudioClip endless_sand;
    private AudioSource audioEndless_sand;
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
        audiolava_city = gameObject.AddComponent<AudioSource>();
        audioShip = gameObject.AddComponent<AudioSource>();
        audioUnderground_cave = gameObject.AddComponent<AudioSource>();
        audioEndless_sand = gameObject.AddComponent<AudioSource>();
        audiocave_crystal = gameObject.AddComponent<AudioSource>();
        audiocyberpunk = gameObject.AddComponent<AudioSource>();
        audiolurid = gameObject.AddComponent<AudioSource>();
        audiosong18d = gameObject.AddComponent<AudioSource>();
        audioxingling = gameObject.AddComponent<AudioSource>();

        audioShip.clip = ship;
        audioShip.loop = true;
        audioShip.volume = 1f;
        audioShip.Play();

        audiolava_city.clip = lava_city;
        audiolava_city.loop = true;
        audiolava_city.volume = 8f;
        audiolava_city.Play();

        audioUnderground_cave.clip = underground_cave;
        audioUnderground_cave.loop = true;
        audioUnderground_cave.volume = 0.5f;
        audioUnderground_cave.Play();

        audioEndless_sand.clip = endless_sand;
        audioEndless_sand.loop = true;
        audioEndless_sand.volume = 6f;
        audioEndless_sand.Play();

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
