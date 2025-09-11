using UnityEngine;

public class Music_menu : MonoBehaviour
{
    public AudioClip nonoCircle;
    private AudioSource audioNonoCircle;
   
    void Start()
    {
        audioNonoCircle = gameObject.AddComponent<AudioSource>();

        audioNonoCircle.clip = nonoCircle;
        audioNonoCircle.loop = true;
        audioNonoCircle.volume = 0.08f;
        audioNonoCircle.Play();
    }
    
}
