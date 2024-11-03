using UnityEngine;
using UnityEngine.UI; // Tambahkan ini jika menggunakan UI Text biasa

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public GameObject gameOverText; // Drag GameOverText dari Hierarchy ke Inspector

    void Start()
    {
        currentHealth = maxHealth;
        gameOverText.SetActive(false); // Pastikan teks tidak aktif di awal
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage! Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
        gameOverText.SetActive(true); // Aktifkan notifikasi saat player mati
        // Tambahkan logika tambahan jika perlu, seperti menghentikan gerakan player
    }
}
