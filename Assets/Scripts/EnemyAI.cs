using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Rigidbody2D rb;
    public Animator anim;

    [Header("Movement")]
    public float moveSpeed = 2.5f;
    public float detectionRange = 6f;
    public float attackRange = 1.2f;

    [Header("Attack")]
    public float attackCooldown = 1f;
    private float attackTimer;

    [Header("Damage")]
    public int damage = 10;
    public float damageRange = 1.2f;

    private bool isFacingRight = true;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (anim == null) anim = GetComponent<Animator>();
    }

    private void Start()
    {
        FindPlayer();
    }

    private void Update()
    {
        if (player == null)
        {
            FindPlayer();
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange && distance > attackRange)
        {
            ChasePlayer();
        }
        else if (distance <= attackRange)
        {
            StopMoving();
            AttackPlayer();
        }
        else
        {
            StopMoving();
        }

        FlipToPlayer();
    }

    private void FindPlayer()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");

        if (p != null)
        {
            player = p.transform;
            Debug.Log("Player ditemukan: " + p.name);
        }
        else
        {
            Debug.LogWarning("Player tidak ditemukan. Cek Tag Player!");
        }
    }

    private void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

        if (anim != null)
            anim.SetBool("EnemyWalk", true);
    }

    private void StopMoving()
    {
        rb.linearVelocity = Vector2.zero;

        if (anim != null)
            anim.SetBool("EnemyWalk", false);
    }

    private void AttackPlayer()
    {
        rb.linearVelocity = Vector2.zero;

        if (anim != null)
            anim.SetBool("EnemyWalk", false);

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            Debug.Log("Enemy menyerang!");

            if (anim != null)
            {
                anim.ResetTrigger("EnemyAttack");
                anim.SetTrigger("EnemyAttack");
            }

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            if (playerHealth != null && Vector2.Distance(transform.position, player.position) <= damageRange)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("HIDUP PRABOWO");
            }

            attackTimer = attackCooldown;
        }
    }

    private void FlipToPlayer()
    {
        if (player.position.x > transform.position.x && !isFacingRight)
            Flip();
        else if (player.position.x < transform.position.x && isFacingRight)
            Flip();
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }
}