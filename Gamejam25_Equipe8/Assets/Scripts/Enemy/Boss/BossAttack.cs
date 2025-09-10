using UnityEngine;
using System.Collections;

public abstract class BossAttack : ScriptableObject
{
    public abstract IEnumerator Execute(
        Transform head,
        Transform leftHand,
        Transform rightHand,
        Transform player
    );
}
