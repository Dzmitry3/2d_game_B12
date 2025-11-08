namespace Enemies
{
    public interface IEnemy
    {
        void Init(EnemyFactory factory, EnemyType type, float speed, int health);
        void ReturnToPool();
    }
}