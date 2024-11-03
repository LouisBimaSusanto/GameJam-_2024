using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    public GameObject weapon; // Drag weapon asset here in the Inspector
    public Animator animator;  // Drag Animator component here in the Inspector
    public float attackRange = 1f; // Jarak serangan
    public LayerMask playerLayer;   // Layer untuk player

    void Start()
    {
        weapon.SetActive(true);
    }

    void Update()
    {
        // Cek apakah player berada dalam jangkauan serangan
        if (IsPlayerInRange())
        {
            Attack();
        }
    }

    void Attack()
    {
        // Trigger animasi serangan
        animator.SetTrigger("AttackTrigger");
        // Logika untuk memberikan damage pada player bisa ditambahkan di sini
        Debug.Log("Enemy attacks with weapon!");
    }

    private bool IsPlayerInRange()
    {
        // Ganti dengan posisi dan ukuran collider sesuai dengan kebutuhan
        return Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
    }

    private void OnDrawGizmosSelected()
    {
        // Untuk visualisasi jangkauan serangan di scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
