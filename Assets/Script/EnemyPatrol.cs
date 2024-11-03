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
    private bool isWaiting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = PointB.transform;
        anim.SetBool("isRunning", true);
    }

    void Update()
    {
        if (!isWaiting)
        {
            Vector2 direction = (currentPoint.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

            if (rb.velocity.magnitude > 0.1f)
            {
                anim.SetBool("isRunning", true); // Set animasi berjalan
            }
            else
            {
                anim.SetBool("isRunning", false); // Set animasi idle jika tidak bergerak
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
            {
                StartCoroutine(WaitAtPoint());
            }
        }
        else
        {
            rb.velocity = Vector2.zero; // Enemy berhenti saat menunggu
            anim.SetBool("isRunning", false); // Set animasi idle
        }
    }

    private IEnumerator WaitAtPoint()
    {
        isWaiting = true;
        rb.velocity = Vector2.zero;
        anim.SetBool("isRunning", false); // Berhenti animasi berjalan, set ke idle

        yield return new WaitForSeconds(2f);

        flip();
        anim.SetBool("isRunning", true); // Mulai kembali animasi berjalan setelah berhenti
        currentPoint = currentPoint == PointB.transform ? PointA.transform : PointB.transform;
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
    }
}
