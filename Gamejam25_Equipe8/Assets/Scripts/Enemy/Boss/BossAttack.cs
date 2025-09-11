using UnityEngine;
using System.Collections;

public abstract class BossAttack : ScriptableObject
{
    [Header("Animação (não usada diretamente pelo SO)")]
    public string animationTrigger;

    public abstract IEnumerator Execute(Transform head, Transform leftHand, Transform rightHand, Transform player);
}
