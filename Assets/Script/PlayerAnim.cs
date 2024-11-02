using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        PlayerEvent.OnIdle += PlayIdleAnimation;
        PlayerEvent.OnRun += PlayRunAnimation;
        PlayerEvent.OnAttack += PlayAttackAnimation;
        PlayerEvent.OnDeath += PlayDeathAnimation;
    }

    private void OnDestroy()
    {
        PlayerEvent.OnRun -= PlayRunAnimation;
        PlayerEvent.OnDeath -= PlayDeathAnimation;
        PlayerEvent.OnIdle -= PlayIdleAnimation;
        PlayerEvent.OnAttack -= PlayAttackAnimation;

    }

    private void PlayRunAnimation()
    {
        animator.SetBool("isRunning", true);
        animator.SetBool("isIdle", false);
    }

    private void PlayIdleAnimation()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isIdle", true);
    }

    private void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    private void PlayDeathAnimation() 
    {
        animator.SetTrigger("Death");
    }
}
