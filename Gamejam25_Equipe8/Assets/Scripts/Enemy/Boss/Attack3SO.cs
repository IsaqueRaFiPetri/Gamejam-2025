using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Boss/Attacks/Attack3")]
public class Attack3SO : BossAttack
{
    public GameObject laserPrefab;
    public float duration = 3f;
    public float telegraphTime = 0.6f;
    public float shakeAmount = 0.15f;
    public float moveSpeed = 2f;
    public AudioClip lazer_beam;

    public override IEnumerator Execute(Transform head, Transform leftHand, Transform rightHand, Transform player)
    {
        Transform[] parts = { head, leftHand, rightHand };
     
        // --- TELEGRAPH ---
        foreach (Transform part in parts)
        {
            Vector3 originalPos = part.position;
            float timer = 0f;
            while (timer < telegraphTime)
            {
                part.position = originalPos + (Vector3)Random.insideUnitCircle * shakeAmount;
                timer += Time.deltaTime;
                yield return null;
            }
            part.position = originalPos;
        }

        // --- DISPARO LASER ---
        GameObject[] activeLasers = new GameObject[parts.Length];
        int[] directions = new int[parts.Length]; 
    
        for (int i = 0; i < parts.Length; i++)
        {
            Transform part = parts[i];

            // direção aleatória (+1 ou -1)
            directions[i] = Random.value > 0.5f ? 1 : -1;
            AudioSource audio = part.GetComponent<AudioSource>();
            if (audio == null)
            {
                audio = part.gameObject.AddComponent<AudioSource>();
                audio.playOnAwake = false;
                audio.loop = false;
                audio.volume = 100f;
            }
            audio.PlayOneShot(lazer_beam);


            // spawn laser
            GameObject laser = Object.Instantiate(laserPrefab, part.position, Quaternion.identity);
            laser.transform.up = (player.position - part.position).normalized;
            activeLasers[i] = laser;
        }

        float elapsed = 0f;
        while (elapsed < duration)
        {
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == head)
                {
                    // cabeça -> mexe no eixo X
                    parts[i].position += Vector3.right * directions[i] * moveSpeed * Time.deltaTime;
                }
                else
                {
                    // mãos -> mexem no eixo Y
                    parts[i].position += Vector3.up * directions[i] * moveSpeed * Time.deltaTime;
                }

                if (activeLasers[i] != null)
                {
                    activeLasers[i].transform.position = parts[i].position;
                }
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // limpar lasers
        for (int i = 0; i < activeLasers.Length; i++)
        {
            if (activeLasers[i] != null)
                Object.Destroy(activeLasers[i].gameObject);
        }
    }
}
