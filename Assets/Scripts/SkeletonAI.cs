using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Rigidbody2D rb;
    public Animator anim;

    [Header("Movement")]
    public float moveSpeed = 2.5f;
    public float detectionRange = 6f;
    public float attackRange = 1.5f;

    [Header("Attack")]
    public int damage = 10;
    public float attackCooldown = 1f;
    public float damageRange = 1.5f;

    [Header("Animator Parameters")]
    public string walkParam = "SkeletonWalk";
    public string attackParam = "SkeletonAttack";

    private float attackTimer;
    private bool isFacingRight = true;
    private bool isDead = false;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (anim == null) anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDead) return;

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                return;
            }
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

    private void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        rb.linearVelocity = new Vector2(
            direction.x * moveSpeed,
            direction.y * moveSpeed
        );

        anim.SetBool(walkParam, true);
    }

    private void StopMoving()
    {
        rb.linearVelocity = Vector2.zero;
        anim.SetBool(walkParam, false);
    }

    private void AttackPlayer()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            anim.ResetTrigger(attackParam);
            anim.SetTrigger(attackParam);

            if (Vector2.Distance(transform.position, player.position) <= damageRange)
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

                if (playerHealth == null)
                    playerHealth = player.GetComponentInParent<PlayerHealth>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
            }

            attackTimer = attackCooldown;
        }
    }

    private void FlipToPlayer()
    {
        if (player == null) return;

        if (player.position.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }

    public void SetDead()
    {
        isDead = true;
        StopMoving();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, damageRange);
    }
}