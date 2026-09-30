using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private TextMeshProUGUI healthUI;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private Animator animator;
    private AudioComposer audioComposer;

    private int currentHealth;
    private bool isDead;

    private void Start()
    {
        audioComposer = FindAnyObjectByType<AudioComposer>();
        animator = GetComponent<Animator>();
        gameOverPanel.SetActive(false);
        uiPanel.SetActive(true);
        currentHealth = maxHealth;
        UpdateUI();
    }

    private void TakeDamage(int amount)
    {
        if (isDead)
            return;

        currentHealth -= amount;
        UpdateUI();
        if(currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("Hurt");
            audioComposer.PlayPlayerHit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Die()
    {
        isDead = true;

        PlayerMovement movement = GetComponent<PlayerMovement>();
        AutoAttack autoAttack = GetComponent<AutoAttack>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Collider2D collider = GetComponent<Collider2D>();

        movement.enabled = false;
        autoAttack.enabled = false;

        rb.linearVelocity = Vector2.zero;
        collider.enabled = false;

        animator.SetTrigger("Death");
        audioComposer.PlayPlayerDeath();

        uiPanel.SetActive(false);

        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        yield return new WaitForSecondsRealtime(0.6f);

        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void UpdateUI()
    {
        healthUI.SetText("Health:" + currentHealth);
    }
}

