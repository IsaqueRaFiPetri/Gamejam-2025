using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Boss/Attacks/Attack1")]
public class Attack1SO : BossAttack
{
    public float lungeSpeed = 6f;
    public float stopDistance = 2f;      // distância mínima do player
    public float telegraphTime = 0.5f;   // tempo de aviso
    public float shakeAmount = 0.15f;    // intensidade da tremida

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

            // --- INVESTIDA ---
            Vector3 target = player.position;
            Vector3 dir = (target - part.position).normalized;

            while (Vector3.Distance(part.position, target) > stopDistance)
            {
                part.position += dir * lungeSpeed * Time.deltaTime;
                yield return null;
            }

            // volta para a posição inicial
            timer = 0f;
            while (timer < 1f)
            {
                timer += Time.deltaTime * (lungeSpeed / 2);
                part.position = Vector3.Lerp(part.position, originalPos, timer);
                yield return null;
            }
        }
    }
}
