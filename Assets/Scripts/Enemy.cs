using UnityEngine; 

public abstract class Enemy : MonoBehaviour, IGivesDamage
{
    [SerializeField] protected Health health;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private int baseDamage = 1;
    [SerializeField] private int minDamage = 1;

    private float velocity = 0;
    private bool recordVel = true;

    protected abstract void Move();

    protected virtual void FixedUpdate()
    {
        Move();

        velocity = recordVel ? Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y) : velocity;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        recordVel = false;

        if (collision.gameObject.TryGetComponent(out IGivesDamage damage))
        {
            damage.GiveDamage(health);
        }
        else
        {
            health.TakeDamage(Mathf.RoundToInt(baseDamage * velocity));
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        recordVel = true;
    }

    public void GiveDamage(Health target)
    {
        int damage = Mathf.RoundToInt(baseDamage * velocity);
        damage = damage >= minDamage ? damage : 0;
        target.TakeDamage(damage);
    }
}
