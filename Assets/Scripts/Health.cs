using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health = 1;
    public int health { get { return _health; } }

    private void Update()
    {
        if (_health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
