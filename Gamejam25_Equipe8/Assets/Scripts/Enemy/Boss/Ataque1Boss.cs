using UnityEngine;
using System.Collections.Generic;

public class Ataque1Boss : MonoBehaviour
{
    public void PerformAttack(Transform head, Transform leftHand, Transform rightHand, float chanceToUseArea)
    {
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
            Debug.Log($"Boss performs Headbutt with {area.name}");
            // TODO: colisão ou animação de ataque
        }
    }
}
