using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayDamageAnimation()
    {
        animator.SetBool("IsTakingDamage", true);
        StartCoroutine(ResetAnimation("IsTakingDamage", 0.5f));
    }

    public void PlayHealAnimation()
    {
        animator.SetBool("IsEating", true);
        StartCoroutine(ResetAnimation("IsEating", 0.5f));
    }

    public void PlayDeathAnimation()
    {
        animator.SetBool("IsDead", true);
    }

    private System.Collections.IEnumerator ResetAnimation(string parameter, float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool(parameter, false);
    }
}
