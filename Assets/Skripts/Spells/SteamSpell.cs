using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SteamSpell : MonoBehaviour
{
    [SerializeField] private AudioComposer audioComposer;
    [SerializeField] private ParticleSystem steamVFX;
    [SerializeField] private float tickCooldown = 1f;
    private float nextTickTime;

    public void TryCast(int fireLevel, int waterLevel)
    {
        if (Time.time < nextTickTime)
        {
            return;
        }

        float damage = GetDamage(fireLevel);
        float radius = GetRadius(waterLevel);

        

        foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, radius))
        {
            if (collider.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
            }
        }
        nextTickTime = Time.time + tickCooldown;
    }

    public void SetActive(bool active, int waterlevel)
    {
        if (active)
        {
            audioComposer.StartSteam();
            float radius = GetRadius(waterlevel);
            ShapeModule shape = steamVFX.shape;
            shape.radius = radius;

            if (!steamVFX.isPlaying)
            {
                steamVFX.Play();
            }
        }
        else
        {
            audioComposer.StopSteam();
            if (steamVFX.isPlaying)
            {
                steamVFX.Stop();
            }
        }
    }

    private float GetDamage(int level)
    {
        switch (level)
        {
            case 1: return 1f;
            case 2: return 2f;
            case 3: return 3f;
            default: return 0f;
        }
    }

    private float GetRadius(int level)
    {
        switch (level)
        {
            case 1: return 4f;
            case 2: return 6f;
            case 3: return 8f;
            default: return 0f;
        }
    }
}
