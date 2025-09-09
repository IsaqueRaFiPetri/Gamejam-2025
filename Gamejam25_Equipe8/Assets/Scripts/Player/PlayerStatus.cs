using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus instance;
    [SerializeField] CharacterStatus playerStatus;
    [SerializeField] Image lifeBar, energyBar;

    float lifeRegenTimer;
    float energyRegenTimer;

    private void Start()
    {
        instance = this;
        playerStatus.life = playerStatus.maxLife;
        playerStatus.energy = playerStatus.maxEnergy;
    }

    private void Update()
    {
        lifeBar.fillAmount = playerStatus.life / playerStatus.maxLife;
        energyBar.fillAmount = playerStatus.energy / playerStatus.maxEnergy;

        if (Troca_Personagens.instance.isHappy)
        {
            RegenerateLife();
            RegenerateEnergy();
        }
    }

    void RegenerateLife()
    {
        lifeRegenTimer += Time.deltaTime;
        if (lifeRegenTimer >= playerStatus.lifeRegenCooldown)
        {
            playerStatus.life = Mathf.Min(playerStatus.life + playerStatus.lifeRegen, playerStatus.maxLife);
            lifeRegenTimer = 0f;
            Debug.Log("Regenerou");
        }
    }

    void RegenerateEnergy()
    {
        energyRegenTimer += Time.deltaTime;
        if (energyRegenTimer >= playerStatus.energyRegenCooldown)
        {
            playerStatus.energy = Mathf.Min(playerStatus.energy + playerStatus.energyRegen, playerStatus.maxEnergy);
            energyRegenTimer = 0f;
        }
    }

    public void TakeDamage(float damage)
    {
        playerStatus.life -= damage;
    }

    public void ReduceEnergy(float reduction)
    {
        playerStatus.energy -= reduction;
    }
}
