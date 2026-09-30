using UnityEngine;

public class FirestormSpell : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private float attackCooldown = 3f;
    [SerializeField] private float coneAngle = 45f;
    [SerializeField] private AudioComposer audioComposer;

    private float nextAttackTime;

    public void TryCast(Transform target, int fireLevel, int airLevel)
    {
        if (Time.time < nextAttackTime)
        {
            return;
        }

        float damage = GetDamage(fireLevel);
        float speed = GetSpeed(airLevel);
        int projectileCount = GetProjectileCount(airLevel);

        Vector3 baseDirection = target.position - transform.position;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle;

            if (projectileCount == 1)
            {
                angle = 0f;
            }
            else
            {
                float t = i / (float)(projectileCount - 1);

                angle = Mathf.Lerp(
                    -coneAngle / 2f,
                    coneAngle / 2f,
                    t
                );
            }

            Vector2 rotatedDirection =
                Quaternion.Euler(0, 0, angle) *
                baseDirection.normalized;

            Projectile newProjectile = Instantiate(
                projectile,
                transform.position,
                Quaternion.identity
            );
            audioComposer.PlayFirestorm();
            newProjectile.SetDamage(damage);
            newProjectile.SetSpeed(speed);

            // Firestorm soll weder Splash noch Pierce haben
            newProjectile.SetPierce(0);
            newProjectile.SetSplashRadius(0f);

            // Immer zuletzt, weil SetDirection die Geschwindigkeit setzt
            newProjectile.SetDirection(rotatedDirection);
        }

        nextAttackTime = Time.time + attackCooldown;
    }


    private float GetDamage(int level)
    {
        switch (level)
        {
            case 1: return 2f;
            case 2: return 3f;
            case 3: return 5f;
            default: return 0f;
        }
    }


    private float GetSpeed(int level)
    {
        switch (level)
        {
            case 1: return 5f;
            case 2: return 6.5f;
            case 3: return 8f;
            default: return 5f;
        }
    }


    private int GetProjectileCount(int level)
    {
        switch (level)
        {
            case 1: return 3;
            case 2: return 5;
            case 3: return 7;
            default: return 0;
        }
    }
}