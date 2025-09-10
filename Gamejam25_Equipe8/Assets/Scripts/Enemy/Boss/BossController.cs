using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class BossController : MonoBehaviour
{
    [Header("References")]
    public CharacterStatus status;
    public Transform head, leftHand, rightHand;
    private Transform player;

    [Header("Attacks (SO)")]
    public BossAttack attack1;
    public BossAttack attack2;
    public BossAttack attack3;

    [Header("Config")]
    [SerializeField] Image bossLifeBar;
    [SerializeField] TextMeshProUGUI bossNameAndPerCent;

    bool hasRegenerated = false;
    float nextAttackTime;

    private void Start()
    {
        player = FindFirstObjectByType<Troca_Personagens>().transform;
        status.life = status.maxLife;
    }

    private void Update()
    {
        bossLifeBar.fillAmount = status.life / status.maxLife;
        int percent = Mathf.RoundToInt((status.life / status.maxLife) * 100f);
        bossNameAndPerCent.SetText($"A Placa-Mãe ({percent}%)");

        if (IsDead) return;

        FollowPlayer();

        if (Time.time >= nextAttackTime)
        {
            BossAttack atk = ChooseNextAttack();
            if (atk != null)
                StartCoroutine(atk.Execute(head, leftHand, rightHand, player));

            nextAttackTime = Time.time + status.cooldown;
        }
    }

    private void FollowPlayer()
    {
        if (player == null) return;

        // mãos seguem no eixo Y
        Vector3 leftPos = leftHand.position;
        leftPos.y = Mathf.Lerp(leftPos.y, player.position.y, Time.deltaTime * status.moveSpeed);
        leftHand.position = leftPos;

        Vector3 rightPos = rightHand.position;
        rightPos.y = Mathf.Lerp(rightPos.y, player.position.y, Time.deltaTime * status.moveSpeed);
        rightHand.position = rightPos;

        // cabeça segue no eixo X
        Vector3 headPos = head.position;
        headPos.x = Mathf.Lerp(headPos.x, player.position.x, Time.deltaTime * status.moveSpeed / 2);
        head.position = headPos;
    }

    private BossAttack ChooseNextAttack()
    {
        float hpPercent = LifePercentage;

        // Regeneração única aos 1%
        if (hpPercent <= 0.01f && !hasRegenerated)
        {
            hasRegenerated = true;
            status.life = status.maxLife * 0.1f;
            Debug.Log("Boss regenerou até 10%!");
        }

        // Fase 1 (100–75%)
        if (hpPercent > 0.75f)
            return Random.value > 0.5f ? attack1 : attack2;

        // Fase 2 (75–40%)
        if (hpPercent > 0.40f)
        {
            float r = Random.value;
            if (r < 0.2f) return attack3;
            return (r < 0.6f) ? attack1 : attack2;
        }

        // Fase 3 (40–1%)
        int choice = Random.Range(0, 3);
        return (choice == 0) ? attack1 : (choice == 1 ? attack2 : attack3);
    }

    public bool IsDead => status.life <= 0;
    public float LifePercentage => status.life / status.maxLife;

    public void TakeDamage(float amount)
    {
        status.life = Mathf.Max(status.life - amount, 0);
        if (IsDead) Die();
    }

    private void Die()
    {
        Debug.Log("Boss derrotado!");
    }
}