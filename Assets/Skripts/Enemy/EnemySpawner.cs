using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform player;

    [Header("Spawn")]
    [SerializeField] private float spawnDistance = 20f;
    [SerializeField] private float startSpawnInterval = 1.2f;
    [SerializeField] private float minSpawnInterval = 0.25f;

    [Header("Enemy")]
    [SerializeField] private float startEnemySpeed = 2f;
    [SerializeField] private float startEnemyHealth = 5f;
    [SerializeField] private float maxEnemySpeed = 4.2f;

    [Header("Difficulty")]
    [SerializeField] private float difficultyInterval = 30f;

    [SerializeField] private float spawnMultiplier = 0.85f;
    [SerializeField] private float speedMultiplier = 1.10f;
    [SerializeField] private float healthMultiplier = 1.15f;

    [Header("Randomness")]
    [SerializeField] private float spawnRandomness = 0.12f;
    [SerializeField] private float speedRandomness = 0.15f;
    [SerializeField] private float healthRandomness = 0.20f;

    private float currentSpawnInterval;
    private float currentEnemySpeed;
    private float currentEnemyHealth;

    private float spawnTimer;
    private float difficultyTimer;

    private int difficultyStage;


    private void Start()
    {
        currentSpawnInterval = startSpawnInterval;
        currentEnemySpeed = startEnemySpeed;
        currentEnemyHealth = startEnemyHealth;

        spawnTimer = currentSpawnInterval;
    }


    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        difficultyTimer += Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemy();
            ResetSpawnTimer();
        }

        if (difficultyTimer >= difficultyInterval)
        {
            IncreaseDifficulty();
            difficultyTimer = 0f;
        }
    }


    private void SpawnEnemy()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        Vector2 spawnPosition =
            (Vector2)player.position
            + randomDirection * spawnDistance;

        GameObject enemy = Instantiate(
            enemyPrefab,
            spawnPosition,
            Quaternion.identity
        );


        // Jeder Enemy bekommt etwas Variation
        float randomSpeed = currentEnemySpeed *
             Random.Range(
                 1f - speedRandomness,
                 1f + speedRandomness
             );

        randomSpeed = Mathf.Min(randomSpeed, maxEnemySpeed);

        float randomHealth = currentEnemyHealth *
            Random.Range(
                1f - healthRandomness,
                1f + healthRandomness
            );


        EnemyMovement movement =
            enemy.GetComponent<EnemyMovement>();

        EnemyHealth health =
            enemy.GetComponent<EnemyHealth>();


        if (movement != null)
        {
            movement.SetMoveSpeed(randomSpeed);
        }

        if (health != null)
        {
            health.SetMaxHealth(randomHealth);
        }
    }


    private void ResetSpawnTimer()
    {
        float randomMultiplier = Random.Range(
            1f - spawnRandomness,
            1f + spawnRandomness
        );

        spawnTimer =
            currentSpawnInterval * randomMultiplier;
    }


    private void IncreaseDifficulty()
    {
        difficultyStage++;

        currentSpawnInterval *= spawnMultiplier;

        currentSpawnInterval = Mathf.Max(
            currentSpawnInterval,
            minSpawnInterval
        );

        currentEnemySpeed *= speedMultiplier;
        currentEnemyHealth *= healthMultiplier;

        Debug.Log(
            $"Difficulty Stage {difficultyStage} | " +
            $"Spawn: {currentSpawnInterval:F2}s | " +
            $"Speed: {currentEnemySpeed:F2} | " +
            $"Health: {currentEnemyHealth:F1}"
        );
    }
}