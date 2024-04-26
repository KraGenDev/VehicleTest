using UnityEngine;

namespace Gameplay.Mechanics.Road
{
    public class RoadObject : MonoBehaviour
    {
        [SerializeField] private RoadEnemies _roadEnemies;
        
        private RoadController _roadController;
        private bool _spawnEnemyOnStart = true;

        private void Start()
        {
            if(_spawnEnemyOnStart)
                SpawnEnemy();
        }

        public void SetRoadController(RoadController roadController) => _roadController = roadController;

        public void SpawnEnemyOnStart(bool value)
        {
            _spawnEnemyOnStart = value;
        }
        
        public void MoveRoadToEnd()
        {
            _roadController.MoveFirstRoadToEnd();
            _roadEnemies.SpawnEnemy();
        }
        
        public void SpawnEnemy() => _roadEnemies.SpawnEnemy();
    }
}