using UnityEngine;
using System.Collections;

public interface IBossAttack
{
    IEnumerator Execute(Transform head, Transform leftHand, Transform rightHand, Transform player);
}

public class BossController : MonoBehaviour
{
    public enum BossState { Idle, Attack1, Attack2, Attack3 }

    [Header("References")]
    public CharacterStatus status;
    public Transform head, leftHand, rightHand;
    Transform player;

    [Header("Configurações")]
    public float timeBetweenAttacks = 2f;

    [Header("Ataques (arraste no Inspector)")]
    public MonoBehaviour attack1Behaviour;
    public MonoBehaviour attack2Behaviour;
    public MonoBehaviour attack3Behaviour;

    private IBossAttack attack1;
    private IBossAttack attack2;
    private IBossAttack attack3;

    private BossState currentState = BossState.Idle;
    private float nextAttackTime;
    private bool hasRegenerated = false;

    private void Start()
    {
        player = FindFirstObjectByType<Troca_Personagens>().transform;
        status.life = status.maxLife;

        attack1 = attack1Behaviour as IBossAttack;
        attack2 = attack2Behaviour as IBossAttack;
        attack3 = attack3Behaviour as IBossAttack;
    }

    private void Update()
    {
        if (IsDead) return;

        FollowPlayer();

        if (currentState == BossState.Idle && Time.time >= nextAttackTime)
        {
            BossState next = ChooseNextAttack();
            if (next != BossState.Idle)
                StartCoroutine(PerformAttack(next));
        }
    }

    private void FollowPlayer()
    {
        if (player == null) return;

        // mãos seguem no Y
        Vector3 leftPos = leftHand.position;
        leftPos.y = Mathf.Lerp(leftPos.y, player.position.y, Time.deltaTime * 2f);
        leftHand.position = leftPos;

        Vector3 rightPos = rightHand.position;
        rightPos.y = Mathf.Lerp(rightPos.y, player.position.y, Time.deltaTime * 2f);
        rightHand.position = rightPos;

        // cabeça segue no X
        Vector3 headPos = head.position;
        headPos.x = Mathf.Lerp(headPos.x, player.position.x, Time.deltaTime * 2f);
        head.position = headPos;
    }

    #region Properties
    public bool IsDead => status.life <= 0;
    public float LifePercentage => status.life / status.maxLife;
    #endregion

    #region Attack Selection
    private BossState ChooseNextAttack()
    {
        float hp = LifePercentage;

        // Fase especial (regeneração)
        if (hp <= 0.01f && !hasRegenerated)
        {
            StartCoroutine(RegeneratePhase());
            return BossState.Idle;
        }

        // Fase 1 (100% → 75%) → sem Attack3
        if (hp > 0.75f)
            return Random.value > 0.5f ? BossState.Attack1 : BossState.Attack2;

        // Fase 2 (75% → 40%) → Attack3 menos frequente
        if (hp > 0.40f)
        {
            float rand = Random.value;
            if (rand < 0.4f) return BossState.Attack3;
            return Random.value > 0.5f ? BossState.Attack1 : BossState.Attack2;
        }

        // Fase 3 (40% → 1%) → todos iguais
        return (BossState)Random.Range(1, 4);
    }
    #endregion

    #region Attack Execution
    private IEnumerator PerformAttack(BossState attack)
    {
        currentState = attack;

        switch (attack)
        {
            case BossState.Attack1:
                if (attack1 != null) yield return StartCoroutine(attack1.Execute(head, leftHand, rightHand, player));
                break;
            case BossState.Attack2:
                if (attack2 != null) yield return StartCoroutine(attack2.Execute(head, leftHand, rightHand, player));
                break;
            case BossState.Attack3:
                if (attack3 != null) yield return StartCoroutine(attack3.Execute(head, leftHand, rightHand, player));
                break;
        }

        yield return new WaitForSeconds(timeBetweenAttacks);
        currentState = BossState.Idle;
        nextAttackTime = Time.time + timeBetweenAttacks;
    }
    #endregion

    #region Special Phase
    private IEnumerator RegeneratePhase()
    {
        hasRegenerated = true;
        Debug.Log("Boss entra em modo regeneração!");

        yield return new WaitForSeconds(2f); // tempo de animação

        status.life = status.maxLife * 0.10f; // volta para 10%
        Debug.Log("Boss regenerou até 10%!");
    }
    #endregion

    #region Combat
    public void TakeDamage(float amount)
    {
        status.life = Mathf.Max(status.life - amount, 0);
        if (IsDead) Die();
    }

    private void Die()
    {
        Debug.Log("Boss derrotado!");
    }
    #endregion
}
