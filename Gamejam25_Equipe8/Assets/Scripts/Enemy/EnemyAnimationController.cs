using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public Animator animator;
    private bool hasSwitched = false;
    [SerializeField] string initialAnim, otherAnim;

    void Start()
    {
        animator.Play(initialAnim);
    }

    public void StartBehavior()
    {
        if (hasSwitched == false)
        {
            animator.Play(otherAnim);
            hasSwitched = true;
        }
    }
}