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

    [Header("others Variables")]
    public float moveSpeed;
    public float thougness;
    public float damage;
    public float cooldown;
    public float regenCooldown;
}