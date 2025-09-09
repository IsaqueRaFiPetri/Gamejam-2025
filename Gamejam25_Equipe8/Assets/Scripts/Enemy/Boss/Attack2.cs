using UnityEngine;
using System.Collections;

public class Attack2 : MonoBehaviour, IBossAttack
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 8f;

    public IEnumerator Execute(Transform head, Transform leftHand, Transform rightHand, Transform player)
    {
        Transform[] parts = { head, leftHand, rightHand };
        int count = Random.Range(1, parts.Length + 1);

        for (int i = 0; i < count; i++)
        {
            Transform part = parts[Random.Range(0, parts.Length)];
            Vector2 dir = (player.position - part.position).normalized;

            GameObject proj = Instantiate(projectilePrefab, part.position, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().linearVelocity = dir * projectileSpeed;
        }

        yield return new WaitForSeconds(0.5f);
    }
}
