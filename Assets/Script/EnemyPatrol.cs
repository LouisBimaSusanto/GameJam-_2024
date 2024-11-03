using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject PointA;
    public GameObject PointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
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
        currentPoint = PointB.transform;
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
        if ((currentPoint.position.x < transform.position.x && transform.localScale.x > 0) ||
            (currentPoint.position.x > transform.position.x && transform.localScale.x < 0))
        {
            flip();
        }

        Vector2 direction = (currentPoint.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

        anim.SetBool("isRunning", rb.velocity.magnitude > 0.1f);

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            StartCoroutine(WaitAtPoint());
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);

        anim.SetBool("isRunning", true);

        // Mengatur arah enemy mengikuti player
        if ((player.position.x < transform.position.x && transform.localScale.x > 0) ||
            (player.position.x > transform.position.x && transform.localScale.x < 0))
        {
            flip();
        }
    }

    // Fungsi untuk mengecek apakah player berada di arah yang sesuai dengan pandangan enemy
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
        rb.velocity = Vector2.zero;
        anim.SetBool("isRunning", false);

        yield return new WaitForSeconds(2f);

        // Setelah selesai menunggu, tentukan target point yang baru
        currentPoint = currentPoint == PointB.transform ? PointA.transform : PointB.transform;

        // Cek arah untuk memastikan menghadap ke target point yang benar
        if ((currentPoint.position.x < transform.position.x && transform.localScale.x > 0) ||
            (currentPoint.position.x > transform.position.x && transform.localScale.x < 0))
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
        if (PointA != null && PointB != null)
        {
            Gizmos.DrawWireSphere(PointA.transform.position, 0.5f);
            Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);
            Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
