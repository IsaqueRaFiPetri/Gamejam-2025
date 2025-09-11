using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Boss/Attacks/Attack1")]
public class Attack1SO : BossAttack
{
    public float lungeSpeed = 6f;
    public float telegraphTime = 0.5f;
    public float shakeAmount = 0.15f;

    [Header("Distâncias máximas")]
    public float maxDistanceHead = 5f;
    public float maxDistanceHands = 3f;

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

            // --- PONTO FINAL (reta até última posição do player, limitado pela distância) ---
            Vector3 playerLastPos = player.position;
            Vector3 direction = (playerLastPos - originalPos).normalized;
            float maxDist = (part == head) ? maxDistanceHead : maxDistanceHands;

            float distanceToTarget = Mathf.Min(Vector3.Distance(originalPos, playerLastPos), maxDist);
            Vector3 target = originalPos + direction * distanceToTarget;

            // --- MOVER PARA FRENTE ---
            while (Vector3.Distance(part.position, target) > 0.01f)
            {
                part.position = Vector3.MoveTowards(part.position, target, lungeSpeed * Time.deltaTime);
                yield return null;
            }
            part.position = target;

            // --- VOLTAR PARA POSIÇÃO INICIAL ---
            while (Vector3.Distance(part.position, originalPos) > 0.01f)
            {
                part.position = Vector3.MoveTowards(part.position, originalPos, (lungeSpeed / 2) * Time.deltaTime);
                yield return null;
            }
            part.position = originalPos;
        }
    }
}
