using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Boss/Attacks/Attack2")]
public class Attack2SO : BossAttack
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 8f;
    public float telegraphTime = 0.4f;
    public float shakeAmount = 0.1f;

    public override IEnumerator Execute(Transform head, Transform leftHand, Transform rightHand, Transform player)
    {
        Transform[] parts = { head, leftHand, rightHand };
        int count = Random.Range(1, parts.Length + 1);

        for (int i = 0; i < count; i++)
        {
            Transform part = parts[Random.Range(0, parts.Length)];

            // --- TELEGRAPH ---
            Vector3 originalPos = part.position;
            float timer = 0f;
            while (timer < telegraphTime)
            {
                part.position = originalPos + (Vector3)Random.insideUnitCircle * shakeAmount;
                timer += Time.deltaTime;
                yield return null;
            }
            part.position = originalPos;

            // --- DISPARO ---
            GameObject proj = Object.Instantiate(projectilePrefab, part.position, Quaternion.identity);
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 dir = (player.position - part.position).normalized;
                rb.linearVelocity = dir * projectileSpeed;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}
