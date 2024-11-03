using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    public int damage = 10;
    private QTEManager qteManager;

    void Start()
    {
        qteManager = FindObjectOfType<QTEManager>(); // Or assign it directly in the Inspector
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                qteManager.StartQTE(enemyHealth);
            }
        }
    }
}
