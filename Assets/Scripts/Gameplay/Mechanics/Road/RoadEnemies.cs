using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Gameplay.Mechanics.Road
{
    public class RoadEnemies : MonoBehaviour
    {
        [SerializeField] private int _minEnemyCount = 4;
        [SerializeField] private int _maxEnemycount = 8;
        [SerializeField] private float _horizontalRange = 3;
        [SerializeField] private float _verticalRange = 10;

        [Inject] private RoadEnemyController _enemyController;
        private List<Enemy.Enemy> _enemies;

        public void SpawnEnemy()
        {
            _enemies ??= new List<Enemy.Enemy>();

            DeactivateActiveEnemy();
            
            var enemyCount = Random.Range(_minEnemyCount, _maxEnemycount + 1);
            
            for (int i = 0; i < enemyCount; i++)
            {
                var enemy = _enemyController.GetEnemy();
                var spawnPoint = this.transform.position;
                
                spawnPoint.x += Random.Range(-_horizontalRange, _horizontalRange);
                spawnPoint.z +=  Random.Range(-_verticalRange, _verticalRange);
                enemy.transform.position = spawnPoint;
                _enemies.Add(enemy);
            }
        }

        private void DeactivateActiveEnemy()
        {
            if (_enemies.Count == 0)
                return;

            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].gameObject.SetActive(false);
            }
            
            _enemies.Clear();
        }
    }
}