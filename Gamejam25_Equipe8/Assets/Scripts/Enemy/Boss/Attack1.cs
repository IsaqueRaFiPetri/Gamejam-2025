using UnityEngine;
using System.Collections;

public class Attack1 : MonoBehaviour, IBossAttack
{
    public float speed = 3f;

    public IEnumerator Execute(Transform head, Transform leftHand, Transform rightHand, Transform player)
    {
        Transform[] parts = { head, leftHand, rightHand };
        int count = Random.Range(1, parts.Length + 1);

        for (int i = 0; i < count; i++)
        {
            Transform part = parts[Random.Range(0, parts.Length)];
            Vector3 start = part.position;
            Vector3 target = player.position;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * speed;
                part.position = Vector3.Lerp(start, target, t);
                yield return null;
            }

            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * speed;
                part.position = Vector3.Lerp(target, start, t);
                yield return null;
            }
        }
    }
}
