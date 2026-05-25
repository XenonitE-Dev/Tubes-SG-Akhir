using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Animator anim;
    public HealthBarUI healthBar;

    private bool isDead = false;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (anim == null)
            anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if (healthBar != null)
            healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        anim.SetTrigger("Hurt");

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        anim.SetTrigger("Die");

        Movement movement = GetComponent<Movement>();
        if (movement != null)
            movement.enabled = false;
    }
}