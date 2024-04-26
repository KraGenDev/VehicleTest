using UnityEngine;

namespace Gameplay.Mechanics.Road
{
    public class RoadEnemyController : MonoBehaviour
    {
        [SerializeField] private Enemy.Enemy _enemyPrefab;
        [SerializeField] private int _countEnemyinPool;
        
        private PoolMono<Enemy.Enemy> _enemyPool;

        private void Awake()
        {
            _enemyPool = new PoolMono<Enemy.Enemy>(_enemyPrefab, _countEnemyinPool)
            {
                AutoExpand = true
            };
        }

        public Enemy.Enemy GetEnemy()
        {
            return _enemyPool.GetFreeElement();
        }
    }
}