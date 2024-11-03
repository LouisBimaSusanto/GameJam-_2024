using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public GameObject attackHitBox;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        attackHitBox.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartAttack();
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndAttack();
        }
    }

    void StartAttack()
    {
        if (spriteRenderer.flipX) // Menghadap kiri
        {
            animator.SetBool("isFacingLeft", true);
            attackHitBox.SetActive (true);
        }
        else // Menghadap kanan
        {
            animator.SetBool("isAttacking", true);
            attackHitBox.SetActive(true);
        }
    }

    public void EndAttack()
    {
        animator.SetBool("isAttacking", false);
        animator.SetBool("isFacingLeft", false);
        attackHitBox.SetActive (false);
    }
}
