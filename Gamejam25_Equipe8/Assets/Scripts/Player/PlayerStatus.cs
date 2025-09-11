using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus instance;

    [SerializeField] public CharacterStatus playerStatus;
    [SerializeField] Image lifeBar, energyBar, emotionBar;
    [SerializeField] GameObject defeatPainel;

    private bool collectedBrave, collectedFear, collectedHappy, collectedSad;

    private void Start()
    {
        instance = this;
        playerStatus.life = playerStatus.maxLife;
        playerStatus.energy = playerStatus.maxEnergy;
        playerStatus.emotion = 380;
    }

    private void Update()
    {
        UpdateUI();
        ReduceEmotionOverTime();
        CheckEmotionCollection();
    }

    void UpdateUI()
    {
        lifeBar.fillAmount = playerStatus.life / playerStatus.maxLife;
        energyBar.fillAmount = playerStatus.energy / playerStatus.maxEnergy;
        emotionBar.fillAmount = playerStatus.emotion / playerStatus.maxEmotion;
    }

    public void RegenerateStats()
    {
        playerStatus.life = Mathf.Min(playerStatus.life + playerStatus.lifeRegen, playerStatus.maxLife);
        playerStatus.energy = Mathf.Min(playerStatus.energy + playerStatus.energyRegen, playerStatus.maxEnergy);
        playerStatus.emotion = Mathf.Min(playerStatus.emotion + playerStatus.emotionRegen, playerStatus.maxEmotion);
    }

    void ReduceEmotionOverTime()
    {
        playerStatus.emotion -= 1f * Time.deltaTime;
        playerStatus.emotion = Mathf.Max(playerStatus.emotion, 0f);
    }

    public void TakeDamageplayer(float damage)
    {
        Troca_Personagens.instance.ActivateBrave();

        if (Troca_Personagens.instance.isSad)
            playerStatus.life -= damage / (playerStatus.toughness * 2);
        else
            playerStatus.life -= damage / playerStatus.toughness;

        if (playerStatus.life <= 0)
        {
            Die();
        }
    }

    public void ReduceEnergy(float amount)
    {
        playerStatus.energy -= amount;
    }

    public void CollectEmotion(Emotions emotion)
    {
        switch (emotion)
        {
            case Emotions.Brave: collectedBrave = true; break;
            case Emotions.Fear: collectedFear = true; break;
            case Emotions.Happy: collectedHappy = true; break;
            case Emotions.Sad: collectedSad = true; break;
        }

        if (collectedBrave && collectedFear && collectedHappy && collectedSad)
        {
            if (!playerStatus.allEmotionsCollected)
            {
                playerStatus.allEmotionsCollected = true;
                UnlockNewAbility();
            }
        }
    }

    void UnlockNewAbility()
    {
        playerStatus.emotionTime *= 2;
    }

    void CheckEmotionCollection()
    {
        // Evita sobrescrever emoções temporárias
        if (Troca_Personagens.instance.isBrave || Troca_Personagens.instance.isFear)
            return;

        if (playerStatus.emotion <= 100f)
        {
            Troca_Personagens.instance.ActivateSad();
            if (playerStatus.emotion <= 0f)
            {
                TakeDamageplayer(playerStatus.damage);
            }
        }
        else if (playerStatus.emotion >= 400)
        {
            Troca_Personagens.instance.Swap(Emotions.Happy);
        }
        else
        {
            Troca_Personagens.instance.Swap(Emotions.Normal);
        }
    }

    public void Die()
    {
        defeatPainel.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Troca_Personagens.instance.Swap(Emotions.Fear, true);
        }
    }
}
