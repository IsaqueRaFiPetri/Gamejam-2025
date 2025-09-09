using UnityEngine;
using Yarn;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] CharacterStatus playerStatus;

    private void Start()
    {
        playerStatus.life = playerStatus.maxLife;
        playerStatus.energy = playerStatus.maxEnergy;
    }
}
