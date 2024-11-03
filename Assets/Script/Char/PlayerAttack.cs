using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
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
        animator.SetBool("isAttacking", true);
    }

    public void EndAttack()
    {
        animator.SetBool("isAttacking", false);
    }
}
