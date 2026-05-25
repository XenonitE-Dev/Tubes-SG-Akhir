using UnityEngine;

public class EnemyHealthSkeleton : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 50;
    private int currentHealth;

    [Header("UI")]
    public HealthBarUI healthBar;

    [Header("References")]
    public Animator anim;
    public SkeletonAI skeletonAI;
    public Rigidbody2D rb;
    public Collider2D enemyCollider;

    [Header("Animator Parameters")]
    public string hurtParam = "SkeletonHurt";
    public string dieParam = "SkeletonDie";

    private bool isDead = false;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (anim == null) anim = GetComponent<Animator>();
        if (skeletonAI == null) skeletonAI = GetComponent<SkeletonAI>();
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (enemyCollider == null) enemyCollider = GetComponent<Collider2D>();
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

        if (anim != null)
        {
            anim.ResetTrigger(hurtParam);
            anim.SetTrigger(hurtParam);
        }

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

        if (anim != null)
        {
            anim.ResetTrigger(dieParam);
            anim.SetTrigger(dieParam);
        }

        if (skeletonAI != null)
            skeletonAI.SetDead();

        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        if (enemyCollider != null)
            enemyCollider.enabled = false;

        Destroy(gameObject, 2f);
    }
}