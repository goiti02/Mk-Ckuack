using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gallina_Move : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float enemyBounceForce = 12f;

    [Header("Salud")]
    [SerializeField] private int maxHealth = 100;
    public int currentHealth;

    [Header("Componentes y Checks")]
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private float dirX = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        currentHealth = maxHealth;
        GameLogicManager.Instance?.OnPlayerHealthChanged(currentHealth);
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (dirX > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dirX < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Debug.Log("El jugador ha saltado!");
        }
    }

    public void EnemyBounce()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * enemyBounceForce, ForceMode2D.Impulse);
        Debug.Log($"REBOTE! Se aplicó una fuerza de rebote de {enemyBounceForce}.");
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        GameLogicManager.Instance?.OnPlayerHealthChanged(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        GameLogicManager.Instance?.OnPlayerHealthChanged(currentHealth);
    }

    private void Die()
    {
        GameLogicManager.Instance?.PlayerHasDied();
        gameObject.SetActive(false);
    }
}
