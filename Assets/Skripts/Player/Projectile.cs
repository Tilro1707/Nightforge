using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private int pierce;
    [SerializeField] private float splashRadius;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector2 direction;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetTarget(Transform newTarget)
    {
        direction = ((Vector2)newTarget.position - rb.position).normalized;
        RotateToDirection(direction);
        rb.linearVelocity = direction * speed;
    }

    public void ApplyLevel(ElementType elementType,int level)
    {
        switch (elementType)
        {
            case ElementType.Fire:
                switch (level)
                {
                    case 1:
                        damage = 2;
                        speed = 8;
                        break;
                    case 2:
                        damage = 3;
                        speed = 12;
                        break;
                    case 3:
                        damage = 5;
                        speed = 15;
                        break;
                }
                break;
            case ElementType.Water:
                {
                    switch (level)
                    {
                        case 1:
                            splashRadius = 2;
                            speed = 5;
                            break;
                        case 2:
                            splashRadius = 3;
                            speed = 8;
                            break;
                        case 3:
                            splashRadius = 5;
                            speed = 10;
                            break;
                    }
                    break;
                }
            case ElementType.Air:
                {
                    switch (level)
                    {
                        case 1:
                            pierce = 1;
                            speed = 10;
                            break;
                        case 2:
                            pierce = 3;
                            speed = 15;
                            break;
                        case 3:
                            pierce = 5;
                            speed = 20;
                            break;
                    }
                    break;
                }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            if(splashRadius > 0)
            {
                SplashDamage();
            }
            else
            {
                enemyHealth.TakeDamage(damage);
            }
                
        }

        if (pierce > 0)
        {
            pierce--;
        }
        else
        {
            animator.SetTrigger("Hit");
            rb.linearVelocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 0.2f);
        }
    }

    public void SplashDamage()
    {
        float radius = splashRadius;
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, radius))
        {
            if (collider.CompareTag("Enemy")){
                EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
                if(enemyHealth != null){
                    enemyHealth.TakeDamage(damage);
                }
            }
        }
    }

    public void SetElementColor(ElementType element)
    {
        switch (element)
        {
            case ElementType.Fire:
                spriteRenderer.color = new Color(1f, 0.3f, 0.15f);
                break;

            case ElementType.Water:
                spriteRenderer.color = new Color(0.2f, 0.65f, 1f);
                break;

            case ElementType.Air:
                spriteRenderer.color = new Color(0.75f, 0.9f, 1f);
                break;
        }
    }

    private void RotateToDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetSplashRadius(float splashRadius)
    {
        this.splashRadius = splashRadius;
    }

    public void SetPierce(int pierce)
    {
        this.pierce = pierce;
    }
    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
        RotateToDirection(direction);
        rb.linearVelocity = this.direction * speed;
    }
}