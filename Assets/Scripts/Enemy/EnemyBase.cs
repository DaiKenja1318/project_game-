using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public string enemyType = "Base";
    protected Transform player;
    protected Rigidbody2D rb;
    protected Animator anim;

    [Header("Enemy Stats")]
    public float speed = 2f;
    public int maxHealth = 3;
    protected int currentHealth;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    protected virtual void Update()
    {
        if (!player) return;
        Move();
    }

    protected abstract void Move();

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        OnDespawn();
    }

    public virtual void OnDespawn()
    {
        currentHealth = maxHealth;
        rb.velocity = Vector2.zero;
        EnemyManager.Instance.DespawnEnemy(enemyType, gameObject);
    }
}
