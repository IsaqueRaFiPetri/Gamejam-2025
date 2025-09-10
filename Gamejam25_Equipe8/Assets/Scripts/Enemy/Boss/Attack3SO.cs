using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Boss/Attacks/Attack3")]
public class Attack3SO : BossAttack
{
    public ParticleSystem laserPrefab;
    public float duration = 3f;

    public override IEnumerator Execute(Transform head, Transform leftHand, Transform rightHand, Transform player)
    {
        Transform[] parts = { head, leftHand, rightHand };

        foreach (Transform part in parts)
        {
            ParticleSystem laser = Object.Instantiate(laserPrefab, part.position, Quaternion.identity);
            laser.transform.up = (player.position - part.position).normalized;
            laser.Play();
            Object.Destroy(laser.gameObject, duration);
        }

        yield return new WaitForSeconds(duration);
    }
}
