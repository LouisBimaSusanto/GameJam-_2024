using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashPower = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private bool canDash = true;
    private bool isDashing = false;

    private Rigidbody2D rb;
    private TrailRenderer tr;
    private SpriteRenderer spriteRenderer;

    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        animator.SetBool("isDashing", true);
        yield return new WaitForSeconds(0.2f);

        float dashDirection = spriteRenderer.flipX ? -1f : 1f;

        Vector3 dashPosition = transform.position + new Vector3(dashDirection * dashPower, 0, 0);

        tr.emitting = true;

        transform.position = dashPosition;

        animator.SetBool("isDashing", false);

        yield return new WaitForSeconds(dashDuration);

        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

}
