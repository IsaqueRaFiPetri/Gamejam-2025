using UnityEngine;
public class Som_ambiente : MonoBehaviour
{
    /*public AudioClip[] sonsDFundo;
    private AudioSource[] audioSources;
    */
    public AudioClip nonoCircle;
    public AudioClip music_rain;
    public AudioClip cityScott;
    public AudioClip sonRain;
    public AudioClip pilar;
    public AudioClip sunset;

    private AudioSource audioNonoCircle;
    private AudioSource audioRain;
    private AudioSource audioCityScott;
    private AudioSource audioSonRain;
    private AudioSource audioPilar;
    private AudioSource audioSunset;
    void Start()
    {
        audioNonoCircle = gameObject.AddComponent<AudioSource>();
        audioRain = gameObject.AddComponent<AudioSource>();
        audioCityScott = gameObject.AddComponent<AudioSource>();
        audioSonRain = gameObject.AddComponent<AudioSource>();
        audioPilar = gameObject.AddComponent<AudioSource>();
        audioSunset = gameObject.AddComponent<AudioSource>();

        audioNonoCircle.clip = nonoCircle;
        audioNonoCircle.loop = true;
        audioNonoCircle.volume = 0.08f;
        audioNonoCircle.Play();

        audioRain.clip = music_rain;
        audioRain.loop = true;
        audioRain.volume = 0.48f;
        audioRain.Play();

        audioCityScott.clip = cityScott;
        audioCityScott.loop = true;
        audioCityScott.volume = 0.48f;
        audioCityScott.Play();

        audioSonRain.clip = sonRain;
        audioSonRain.loop = true;
        audioSonRain.volume = 100f;
        audioSonRain.Play();

        audioPilar.clip = pilar;
        audioPilar.loop = true;
        audioPilar.volume = 0.08f;
        audioPilar.Play();

        audioSunset.clip = sunset;
        audioSunset.loop = true;
        audioSunset.volume = 0.0005f;
        audioSunset.Play();

        /*int quantidadeSons = sonsDFundo.Length;
        audioSources = new AudioSource[quantidadeSons];

        for (int i = 0; i < quantidadeSons; i++)
        {           
            AudioSource novoAudioSource = gameObject.AddComponent<AudioSource>();

            novoAudioSource.clip = sonsDFundo[i];
            novoAudioSource.loop = true;
            novoAudioSource.playOnAwake = false;
            novoAudioSource.volume = 0.5f; // Ajuste conforme necessário

            novoAudioSource.Play(); // Toca o som

            audioSources[i] = novoAudioSource;
        }
        */
    }
}

