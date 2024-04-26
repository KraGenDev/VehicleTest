using System;
using Enums;
using UnityEngine;

namespace Gameplay.Mechanics.Road
{
    public class RoadController : MonoBehaviour
    {
        [SerializeField] private RoadObject _roadPrefab;
        [SerializeField] private int _roadCounts;
        [SerializeField] private Transform _roadContainer;
        [SerializeField] private float _spaceBetweenRoads;

        private RoadObject[] _roads;
        private float _lastPositionZ;
        private int _firstRoadIndex;
        private bool _canSwap = true;


        private void OnEnable()
        {
            EventBus.Instance.Subscribe(EventBusAction.Win,DisableRoadSwapping);
            EventBus.Instance.Subscribe(EventBusAction.Lose,DisableRoadSwapping);
        }

        private void OnDisable()
        {
            EventBus.Instance.Unsubscribe(EventBusAction.Win,DisableRoadSwapping);
            EventBus.Instance.Unsubscribe(EventBusAction.Lose, DisableRoadSwapping);
        }

        private void Start()
        {
            CreateRoads();
        }

        private void DisableRoadSwapping() => _canSwap = false;

        public void MoveFirstRoadToEnd()
        {
            if (!_canSwap) 
                return;
            
            var road = _roads[_firstRoadIndex];
            road.transform.position = new Vector3(0, 0, _lastPositionZ);

            _firstRoadIndex = (_firstRoadIndex + 1) % _roads.Length;
            _lastPositionZ += _spaceBetweenRoads;
        }

        private void CreateRoads()
        {
            var lastPosition = Vector3.zero;
            _roads = new RoadObject[_roadCounts];
            
            for (int i = 0; i < _roadCounts; i++)
            {
                var road = Instantiate(_roadPrefab, lastPosition, Quaternion.identity, _roadContainer);
                road.SpawnEnemyOnStart(i > 0);
                road.SetRoadController(this);
                
                _roads[i] = road;
                lastPosition.z += _spaceBetweenRoads;
            }

            _lastPositionZ = lastPosition.z;
        }
    }
}