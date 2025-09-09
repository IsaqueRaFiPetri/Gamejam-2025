using UnityEngine;
using System.Collections.Generic;

public class Ataque2Boss : MonoBehaviour
{
    public void PerformAttack(Transform head, Transform leftHand, Transform rightHand,
                              Transform player, GameObject projectilePrefab, float chanceToUseArea)
    {
        if (player == null || projectilePrefab == null) return;

        List<Transform> areas = new List<Transform> { head, leftHand, rightHand };
        List<Transform> chosenAreas = new List<Transform>();

        foreach (Transform area in areas)
        {
            if (Random.value <= chanceToUseArea)
                chosenAreas.Add(area);
        }

        if (chosenAreas.Count == 0)
            chosenAreas.Add(areas[Random.Range(0, areas.Count)]);

        foreach (Transform area in chosenAreas)
        {
            GameObject proj = Instantiate(projectilePrefab, area.position, Quaternion.identity);
            Vector3 dir = (player.position - area.position).normalized;
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = dir * 10f; // velocidade do projétil
            Debug.Log($"Boss fires projectile from {area.name}");
        }
    }
}
