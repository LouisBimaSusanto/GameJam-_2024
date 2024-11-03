using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    public GameObject weapon; // Drag weapon asset here in the Inspector
    public Animator animator;  // Drag Animator component here in the Inspector
    public float attackRange = 1f; // Jarak serangan
    public LayerMask playerLayer;   // Layer untuk player
    public int damageAmount = 20; // Jumlah damage yang diberikan

    void Start()
    {
        weapon.SetActive(true);
    }

    void Update()
    {
        if (IsPlayerInRange())
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("AttackTrigger");
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);

        if (playerCollider != null)
        {
            PlayerHealth playerHealth = playerCollider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }

    private bool IsPlayerInRange()
    {
        return Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
