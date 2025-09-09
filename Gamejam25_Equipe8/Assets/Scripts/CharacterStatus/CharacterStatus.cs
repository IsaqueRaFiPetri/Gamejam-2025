using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatus", menuName = "Scriptable Objects/CharacterStatus")]
public class CharacterStatus : ScriptableObject
{
    public float life;
    public float lifeRegen;
    public float energy;
    public float energyRegen;
    public float moveSpeed;
    public float thougness;
    public float damage;
    public float cooldown;
}