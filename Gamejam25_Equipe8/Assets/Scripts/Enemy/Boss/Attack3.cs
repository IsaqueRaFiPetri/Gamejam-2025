using UnityEngine;
using System.Collections;

public class Attack3 : MonoBehaviour, IBossAttack
{
    public ParticleSystem laserPrefab;
    public float duration = 3f;

    public IEnumerator Execute(Transform head, Transform leftHand, Transform rightHand, Transform player)
    {
        ParticleSystem headLaser = Instantiate(laserPrefab, head.position, Quaternion.identity);
        ParticleSystem leftLaser = Instantiate(laserPrefab, leftHand.position, Quaternion.identity);
        ParticleSystem rightLaser = Instantiate(laserPrefab, rightHand.position, Quaternion.identity);

        headLaser.transform.up = (player.position - head.position).normalized;
        leftLaser.transform.up = (player.position - leftHand.position).normalized;
        rightLaser.transform.up = (player.position - rightHand.position).normalized;

        headLaser.Play();
        leftLaser.Play();
        rightLaser.Play();

        yield return new WaitForSeconds(duration);

        Destroy(headLaser.gameObject);
        Destroy(leftLaser.gameObject);
        Destroy(rightLaser.gameObject);
    }
}
