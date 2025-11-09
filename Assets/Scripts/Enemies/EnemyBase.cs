using System;
using UnityEngine;


namespace Enemies
{
    public abstract class EnemyBase : MonoBehaviour, IEnemy
    {
        protected float _speed;
        protected float _health;
        private EnemyFactory _factory;
        private EnemyType _type;
        public abstract void Move();
        
        public virtual void Init(EnemyFactory factory, EnemyType type, float speed, int health)
        {
            _factory = factory;
            _type = type;
            _speed = speed;
            _health = health;
        }
        
        public void ReturnToPool()
        {
            _factory.ReturnToPool(_type, gameObject);
        }

        protected virtual void Die()
        {
            _factory.ReturnToPool(_type, gameObject);
        }
        public virtual void TakeDamage(float amount)
        {
            _health -= amount;
            if (_health <= 0)
                Die();
        }
    }
}