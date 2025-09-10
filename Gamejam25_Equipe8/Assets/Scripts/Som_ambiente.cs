using UnityEngine;
public class Som_ambiente : MonoBehaviour
{
    /*public AudioClip[] sonsDFundo;
    private AudioSource[] audioSources;
    */
    public AudioClip nonoCircle;
    public AudioClip music_rain;
    public AudioClip cityScott;
    public AudioClip vent;
    public AudioClip pilar;
    public AudioClip sunset;
    public AudioClip chuva_sonoro;
    public AudioClip rain_2;

    private AudioSource audioNonoCircle;
    private AudioSource audioRain;
    private AudioSource audioCityScott;
    private AudioSource audioVent;
    private AudioSource audioPilar;
    private AudioSource audioSunset;
    private AudioSource audioChuva_Sonoro;
    private AudioSource audioRain_2;
    void Start()
    {
        audioNonoCircle = gameObject.AddComponent<AudioSource>();
        audioRain = gameObject.AddComponent<AudioSource>();
        audioCityScott = gameObject.AddComponent<AudioSource>();
        audioVent = gameObject.AddComponent<AudioSource>();
        audioPilar = gameObject.AddComponent<AudioSource>();
        audioSunset = gameObject.AddComponent<AudioSource>();
        audioChuva_Sonoro = gameObject.AddComponent<AudioSource>();
        audioRain_2 = gameObject.AddComponent<AudioSource>();

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

        audioVent.clip = vent;
        audioVent.loop = true;
        audioVent.volume = 100f;
        audioVent.Play();

        audioPilar.clip = pilar;
        audioPilar.loop = true;
        audioPilar.volume = 0.08f;
        audioPilar.Play();

        audioSunset.clip = sunset;
        audioSunset.loop = true;
        audioSunset.volume = 0.0005f;
        audioSunset.Play();

        audioChuva_Sonoro.clip = chuva_sonoro;
        audioChuva_Sonoro.loop = true;
        audioChuva_Sonoro.volume = 0.8f;
        audioChuva_Sonoro.Play();

        audioRain_2.clip = rain_2;
        audioRain_2.loop = true;
        audioRain_2.volume = 0.05f;
        audioRain_2.Play();

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

