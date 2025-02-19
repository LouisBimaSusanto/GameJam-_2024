using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints; // Array untuk point yang bisa dibagikan
    private int currentPointIndex = 0; // Index untuk point saat ini
    private Rigidbody2D rb;
    private Animator anim;
    public float speed;
    public float chaseSpeed;
    private bool isWaiting = false;

    public Transform player; // Referensi untuk player
    public float chaseRange; // Jarak deteksi untuk mengejar
    private bool isChasing = false; // Status apakah enemy sedang mengejar

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Cek jika player berada di dalam range chase dan di arah yang sesuai
        if (distanceToPlayer < chaseRange && IsPlayerInCorrectDirection())
        {
            isChasing = true;
        }
        else if (isChasing && (distanceToPlayer >= chaseRange + 2f || !IsPlayerInCorrectDirection()))
        {
            isChasing = false;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else if (!isWaiting)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        // Tentukan arah ke target patrol dan cek apakah perlu flip
        Transform targetPoint = patrolPoints[currentPointIndex];
        if ((targetPoint.position.x < transform.position.x && transform.localScale.x > 0) ||
            (targetPoint.position.x > transform.position.x && transform.localScale.x < 0))
        {
            flip();
        }

        Vector2 direction = (targetPoint.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);

        anim.SetBool("isRunning", rb.linearVelocity.magnitude > 0.1f);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.5f)
        {
            StartCoroutine(WaitAtPoint());
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * chaseSpeed, rb.linearVelocity.y);

        anim.SetBool("isRunning", true);

        // Mengatur arah enemy mengikuti player
        if ((player.position.x < transform.position.x && transform.localScale.x > 0) ||
            (player.position.x > transform.position.x && transform.localScale.x < 0))
        {
            flip();
        }
    }

    bool IsPlayerInCorrectDirection()
    {
        if ((player.position.x < transform.position.x && transform.localScale.x < 0) ||
            (player.position.x > transform.position.x && transform.localScale.x > 0))
        {
            return true;
        }
        return false;
    }

    private IEnumerator WaitAtPoint()
    {
        isWaiting = true;
        rb.linearVelocity = Vector2.zero;
        anim.SetBool("isRunning", false);

        yield return new WaitForSeconds(2f);

        // Berpindah ke point berikutnya dalam array
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;

        // Cek arah untuk memastikan menghadap ke target point yang benar
        Transform targetPoint = patrolPoints[currentPointIndex];
        if ((targetPoint.position.x < transform.position.x && transform.localScale.x > 0) ||
            (targetPoint.position.x > transform.position.x && transform.localScale.x < 0))
        {
            flip();
        }

        anim.SetBool("isRunning", true);
        isWaiting = false;
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        // Menampilkan semua titik patrol di editor
        Gizmos.color = Color.blue;
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            if (patrolPoints[i] != null)
            {
                Gizmos.DrawWireSphere(patrolPoints[i].position, 0.5f);
                if (i < patrolPoints.Length - 1)
                {
                    Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
                }
            }
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}