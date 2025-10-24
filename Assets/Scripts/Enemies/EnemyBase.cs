using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected int _health;

    public abstract void Move();
    public virtual void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
    
    
}