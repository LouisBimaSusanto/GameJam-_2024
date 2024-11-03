using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{
    protected Rigidbody2D rb;
    public float moveSpeed = 5f;
    private float movement;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer childSpriteRenderer; // Referensi ke SpriteRenderer pada child object
    private Animator animator;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        childSpriteRenderer = transform.Find("Sabit").GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxisRaw("Horizontal");

        if (movement != 0)
        {
            spriteRenderer.flipX = movement < 0;
            childSpriteRenderer.flipX = movement < 0;
            animator.SetBool("isRunning", true);
        }
        else if (isGrounded)
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement * moveSpeed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
