using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxhealth = 5f;
    [SerializeField] private int soulReward = 1;
    private Animator animator;
    private AudioComposer audioComposer;

    private float currentHealth;
    private bool isDead;

    private void Start()
    {
        currentHealth = maxhealth;
        audioComposer = FindAnyObjectByType<AudioComposer>();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float value)
    {
        if (isDead)
            return;

        currentHealth -= value;
        
        if (currentHealth <= 0)
        {
            audioComposer.PlayEnemyDeath();
            Die();
        }
        else
        {
            animator.SetTrigger("Hurt");
            audioComposer.PlayEnemyHit();
        }
    }

    private void Die()
    {
        isDead = true;

        SoulManager souls = FindFirstObjectByType<SoulManager>();
        souls.AddSouls(soulReward);

        animator.SetTrigger("Death");

        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject, 0.6f);
    }

    public void SetMaxHealth(float health)
    {
        maxhealth = health;
        currentHealth = health;
    }
}
