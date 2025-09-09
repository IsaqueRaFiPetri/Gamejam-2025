using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    public enum BossState { Idle, Attack1, Attack2, Attack3 }

    [Header("References")]
    public CharacterStatus status;
    public Transform headArea;
    public Transform leftHandArea;
    public Transform rightHandArea;
    Transform player;
    public Ataque3Boss ataque3Component;

    [Header("Prefabs e Configurações")]
    public GameObject projectilePrefab;
    public float timeBetweenAttacks = 2f;
    [Range(0f, 1f)] public float chanceToUseArea = 0.5f;

    private BossState currentState = BossState.Idle;
    private float nextAttackTime;

    [Header("Attack Components")]
    public Ataque1Boss ataque1Component;
    public Ataque2Boss ataque2Component;

    private void Start()
    {
        if (status == null)
        {
            Debug.LogError("BossController: CharacterStatus missing!");
            enabled = false;
            return;
        }

        status.life = status.maxLife;
        currentState = BossState.Idle;

        player = FindFirstObjectByType<Troca_Personagens>().transform;
    }

    private void Update()
    {
        if (IsDead) return;

        if (currentState == BossState.Idle && Time.time >= nextAttackTime)
        {
            BossState next = ChooseNextAttack();
            StartCoroutine(PerformAttack(next));
            nextAttackTime = Time.time + timeBetweenAttacks;
        }
    }

    #region Properties
    public bool IsDead => status.life <= 0;
    public float LifePercentage => status.life / status.maxLife;
    #endregion

    #region Attack Selection
    private BossState ChooseNextAttack()
    {
        if (LifePercentage > 0.75f)
            return (Random.value > 0.5f) ? BossState.Attack1 : BossState.Attack2;
        else if (LifePercentage > 0.5f)
        {
            int choice = Random.Range(0, 3);
            return (BossState)choice;
        }
        else
        {
            float rand = Random.value;
            if (rand < 0.5f) return BossState.Attack3;
            return (Random.value > 0.5f) ? BossState.Attack1 : BossState.Attack2;
        }
    }
    #endregion

    #region Attack Coroutine
    private IEnumerator PerformAttack(BossState attack)
    {
        currentState = attack;

        switch (attack)
        {
            case BossState.Attack1:
                ataque1Component?.PerformAttack(headArea, leftHandArea, rightHandArea, chanceToUseArea);
                break;
            case BossState.Attack2:
                ataque2Component?.PerformAttack(headArea, leftHandArea, rightHandArea, player, projectilePrefab, chanceToUseArea);
                break;
            case BossState.Attack3:
                if (ataque3Component != null)
                {
                    ataque3Component.StartAttack();
                    yield return new WaitForSeconds(3f); // duração do ataque3
                    ataque3Component.StopAttack();
                }
                break;
        }

        yield return new WaitForSeconds(timeBetweenAttacks);
        currentState = BossState.Idle;
    }
    #endregion

    #region Combat
    public void TakeDamage(float amount)
    {
        status.life -= amount;
        status.life = Mathf.Max(status.life, 0);

        if (IsDead) Die();
    }

    private void Die()
    {
        Debug.Log("Boss defeated!");
        // TODO: animação de morte, loot, etc.
    }
    #endregion
}