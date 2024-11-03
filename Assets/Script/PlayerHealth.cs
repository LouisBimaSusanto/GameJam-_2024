using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Disappear();
        }
    }

    void Disappear()
    {
        // Menghilangkan player dari scene
        Destroy(gameObject);
        Debug.Log("Player hilang dari scene!");
    }
}
