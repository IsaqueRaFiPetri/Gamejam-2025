using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Boss/Attacks/Attack2")]
public class Attack2SO : BossAttack
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 8f;
    public float telegraphTime = 0.4f;
    public float shakeAmount = 0.15f;
    public AudioClip lazer_ball;   

    public override IEnumerator Execute(Transform head, Transform leftHand, Transform rightHand, Transform player)
    {
        Transform[] parts = { head, leftHand, rightHand };
        int count = Random.Range(1, parts.Length + 1);    
           
        for (int i = 0; i < count; i++)
        {
            Transform part = parts[Random.Range(0, parts.Length)];
            Vector3 originalPos = part.position;

            // --- TELEGRAPH ---
            float timer = 0f;
            while (timer < telegraphTime)
            {
                part.position = originalPos + (Vector3)Random.insideUnitCircle * shakeAmount;
                timer += Time.deltaTime;
                yield return null;
            }
            part.position = originalPos;
            
            AudioSource audio = part.GetComponent<AudioSource>();
            if (audio == null)
            {
                audio = part.gameObject.AddComponent<AudioSource>();
                audio.playOnAwake = false;
                audio.loop = false;
                audio.volume = 100f;
            }
            audio.PlayOneShot(lazer_ball);

            // --- PROJETIL (direção fixa para última posição do player) ---
            Vector3 playerLastPos = player.position;
            Vector2 dir = (playerLastPos - part.position).normalized;

            GameObject proj = Object.Instantiate(projectilePrefab, part.position, Quaternion.identity);
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = dir * projectileSpeed;

            yield return new WaitForSeconds(0.2f);
        }
    }
}
