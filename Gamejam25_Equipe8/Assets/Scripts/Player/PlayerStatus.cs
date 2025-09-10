using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus instance;

    [SerializeField] public CharacterStatus playerStatus;
    [SerializeField] Image lifeBar, energyBar, emotionBar;

    // Flags de emoções coletadas
    private bool collectedBrave, collectedFear, collectedHappy, collectedSad;

    private void Start()
    {
        instance = this;
        playerStatus.life = playerStatus.maxLife;
        playerStatus.energy = playerStatus.maxEnergy;
        playerStatus.emotion = 74;
    }

    private void Update()
    {
        UpdateUI();
        ReduceEmotionOverTime();
        CheckEmotionCollection();

        if(Troca_Personagens.instance.isHappy)
            RegenerateStats();
    }

    void UpdateUI()
    {
        lifeBar.fillAmount = playerStatus.life / playerStatus.maxLife;
        energyBar.fillAmount = playerStatus.energy / playerStatus.maxEnergy;
        emotionBar.fillAmount = playerStatus.emotion / playerStatus.maxEmotion;
    }

    void RegenerateStats()
    {
        if (Troca_Personagens.instance.isHappy)
        {
            playerStatus.life = Mathf.Min(playerStatus.life + playerStatus.lifeRegen * Time.deltaTime, playerStatus.maxLife);
            playerStatus.energy = Mathf.Min(playerStatus.energy + playerStatus.energyRegen * Time.deltaTime, playerStatus.maxEnergy);
        }
    }

    void ReduceEmotionOverTime()
    {
        playerStatus.emotion -= 1f * Time.deltaTime;
        playerStatus.emotion = Mathf.Max(playerStatus.emotion, 0f);
    }

    public void TakeDamageplayer(float damage)
    {
        playerStatus.life -= damage;
        // Brave é ativado sempre que o jogador é atingido
        Troca_Personagens.instance.ActivateBrave();
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
        // Sad automático quando a barra estiver baixa
        if (playerStatus.emotion <= 25f)
        {
            Troca_Personagens.instance.ActivateSad();
            if (playerStatus.emotion <= 0f)
            {
                playerStatus.life = 0f;
            }
        }
        else if(playerStatus.emotion >= 75)
            Troca_Personagens.instance.Swap(Emotions.Happy);
        else
            Troca_Personagens.instance.Swap(Emotions.Normal);
    }
}
