using System.Collections;
using UnityEngine;

public class Slime : Enemy
{
    [Header("References")]
    public Rigidbody2D rb;
    public Animator anim;
    public Transform player;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Transform[] movePoints;

    [Header("Settings")]
    public float moveSpeed = 2f;
    public float detectionRange = 8f;
    public float attackRange = 3f;
    public float jumpForce = 8f;
    public float attackJumpForce = 12f;

    private Vector2 velocity;
    private bool isGrounded;
    private bool isAttacking;
    private float groundCheckRadius = 0.2f;

    void Start()
    {
        base.Intialize(50);
        DamgeHit = 30;
        HP.maxValue = 50;

        // เริ่มเดินไปทางซ้าย
        velocity = new Vector2(-moveSpeed, 0);
    }

    void Update()
    {
        CheckGround();
        HandleAnimator();
        HP.value = Health;
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange && isGrounded)
        {
            StartCoroutine(JumpAttack());
        }
        else if (distance <= detectionRange && !isAttacking)
        {
            MoveTowardsPlayer();
        }
        else if (!isAttacking)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        rb.velocity = new Vector2(velocity.x, rb.velocity.y);

        if (velocity.x < 0 && transform.position.x <= movePoints[0].position.x) Flip();
        if (velocity.x > 0 && transform.position.x >= movePoints[1].position.x) Flip();
    }

    void MoveTowardsPlayer()
    {
        if (isAttacking) return;

        Vector2 dir = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(dir.x * moveSpeed, rb.velocity.y);

        // หันตัวซ้าย/ขวา
        transform.localScale = new Vector3(Mathf.Sign(dir.x), 1, 1);
    }

    IEnumerator JumpAttack()
    {
        if (isAttacking) yield break;
        isAttacking = true;

        anim.SetTrigger("JumpPrepare");
        yield return new WaitForSeconds(0.3f);

        Vector2 jumpDir = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(jumpDir.x * 2f, attackJumpForce);

        anim.SetTrigger("JumpAttack");
        yield return new WaitForSeconds(1f);

        isAttacking = false;
    }

    void Flip()
    {
        velocity.x *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void HandleAnimator()
    {
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("ground", isGrounded);

        if (Mathf.Abs(rb.velocity.x) < 0.1f && isGrounded && !isAttacking)
            anim.Play("Green Idle");
    }

    public override void TakeDamage(int damage)
    {
        if (!alive) return;

        Health -= damage;

        if (Health > 0)
        {
            if (anim != null) anim.SetTrigger("GreenHurt");
        }
        else
        {
            alive = false;
            if (anim != null) anim.SetTrigger("GreenDeath");
            OnDeath();
        }
    }

    protected override void OnDeath()
    {
        this.enabled = false;

        if (HP != null)
            Destroy(HP.gameObject);

        Destroy(gameObject, 1.5f);
    }

    public override void Behavior()
    {
        // รวมไว้ใน Update แล้ว ไม่ต้อง implement เพิ่ม
    }

    void OnDrawGizmosSelected()
    {
        if (player == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
