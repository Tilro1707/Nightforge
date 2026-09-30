using UnityEngine;

public class JetLanceSpell : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private float attackCooldown = 3f;
    [SerializeField] private AudioComposer audioComposer;
    private float nextAttackTime;

    public void TryCast(Transform target, int waterLevel, int airLevel)
    {
        if (Time.time < nextAttackTime)
        {
            return;
        }

        float damage = 3f;
        float splashRadius = GetSplashRadius(waterLevel);
        float speed = GetSpeed(airLevel);
        int pierce = GetPierce(airLevel);

        Projectile newProjectile = Instantiate(
            projectile,
            transform.position,
            Quaternion.identity
        );
        audioComposer.PlayJetLance();
        newProjectile.SetDamage(damage);
        newProjectile.SetSplashRadius(splashRadius);
        newProjectile.SetSpeed(speed);
        newProjectile.SetPierce(pierce);

        newProjectile.SetTarget(target);

        nextAttackTime = Time.time + attackCooldown;
    }

    private float GetSplashRadius(int level)
    {
        switch (level)
        {
            case 1: return 1.5f;
            case 2: return 2.5f;
            case 3: return 4f;
            default: return 0f;
        }
    }

    private float GetSpeed(int level)
    {
        switch (level)
        {
            case 1: return 12f;
            case 2: return 16f;
            case 3: return 20f;
            default: return 8f;
        }
    }

    private int GetPierce(int level)
    {
        switch (level)
        {
            case 1: return 3;
            case 2: return 5;
            case 3: return 8;
            default: return 0;
        }
    }
}