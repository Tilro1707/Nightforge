using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private Projectile basicProjectile;

    [Header("Spells")]
    [SerializeField] private SteamSpell steamSpell;
    [SerializeField] private JetLanceSpell jetLanceSpell;
    [SerializeField] private FirestormSpell firestormSpell;

    [Header("Basic Attack")]
    [SerializeField] private float basicAttackCooldown = 1f;

    private float basicAttackTimer;


    private void Update()
    {
        basicAttackTimer -= Time.deltaTime;

        int fireLevel = upgradeManager.GetLevel(ElementType.Fire);
        int waterLevel = upgradeManager.GetLevel(ElementType.Water);
        int airLevel = upgradeManager.GetLevel(ElementType.Air);
        bool steamActive = fireLevel > 0 && waterLevel > 0;
        steamSpell.SetActive(steamActive, waterLevel);
        int activeElements = 0;

        if (fireLevel > 0)
            activeElements++;

        if (waterLevel > 0)
            activeElements++;

        if (airLevel > 0)
            activeElements++;
        
        switch (activeElements)
        {
            case 0:
                break;

            case 1:
                HandleBasicAttack(fireLevel, waterLevel, airLevel);
                break;

            case 2:
                HandleComboAttack(fireLevel, waterLevel, airLevel);
                break;

            case 3:
                HandleAllCombos(fireLevel, waterLevel, airLevel);
                break;
        }
    }


    private void HandleBasicAttack(int fireLevel, int waterLevel, int airLevel)
    {
        if (fireLevel > 0)
        {
            TryBasicAttack(ElementType.Fire, fireLevel);
        }
        else if (waterLevel > 0)
        {
            TryBasicAttack(ElementType.Water, waterLevel);
        }
        else if (airLevel > 0)
        {
            TryBasicAttack(ElementType.Air, airLevel);
        }
    }


    private void HandleComboAttack(int fireLevel, int waterLevel, int airLevel)
    {
        if (fireLevel > 0 && waterLevel > 0)
        {
            steamSpell.TryCast(fireLevel, waterLevel);
            
        }
        else if (fireLevel > 0 && airLevel > 0)
        {
            Transform target = FindNearestEnemy();

            if (target != null)
            {
                firestormSpell.TryCast(target, fireLevel, airLevel);
            }
        }
        else if (waterLevel > 0 && airLevel > 0)
        {
            Transform target = FindNearestEnemy();

            if (target != null)
            {
                jetLanceSpell.TryCast(target, waterLevel, airLevel);
            }
        }
    }


    private void HandleAllCombos(int fireLevel, int waterLevel, int airLevel)
    {
        steamSpell.TryCast(fireLevel, waterLevel);
        


        Transform target = FindNearestEnemy();

        if (target != null)
        {
            firestormSpell.TryCast(target, fireLevel, airLevel);
            jetLanceSpell.TryCast(target, waterLevel, airLevel);
        }
    }


    private void TryBasicAttack(ElementType element, int level)
    {
        if (basicAttackTimer > 0)
            return;

        Transform target = FindNearestEnemy();

        if (target == null)
            return;

        Projectile newProjectile = Instantiate(
            basicProjectile,
            transform.position,
            Quaternion.identity
        );

        newProjectile.ApplyLevel(element, level);
        newProjectile.SetElementColor(element);
        newProjectile.SetTarget(target);

        basicAttackTimer = basicAttackCooldown;
    }


    private Transform FindNearestEnemy()
    {
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(
                transform.position,
                enemy.transform.position
            );

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }
}