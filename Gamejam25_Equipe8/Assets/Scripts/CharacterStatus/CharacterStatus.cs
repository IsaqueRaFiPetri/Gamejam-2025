using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatus", menuName = "Scriptable Objects/CharacterStatus")]
public class CharacterStatus : ScriptableObject
{
    [Header("Life")]
    public float life;
    public float maxLife;
    public float lifeRegen;

    [Header("Energy")]
    public float energy;
    public float maxEnergy;
    public float energyRegen;

    [Header("Emotional")]
    [HideInInspector] public float emotion;
    public float maxEmotion;
    public float emotionRegen;
    public float emotionTime;
    [HideInInspector] public bool allEmotionsCollected;

    [Header("Others Variables")]
    public float moveSpeed;
    public float toughness;
    public float damage;
    public float cooldown;
    public float regenCooldown;
}