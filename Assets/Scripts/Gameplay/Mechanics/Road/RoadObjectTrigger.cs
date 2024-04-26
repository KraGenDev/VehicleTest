using Gameplay.Mechanics.Vehicles;
using UnityEngine;

namespace Gameplay.Mechanics.Road
{
    [RequireComponent(typeof(Collider))]
    public class RoadObjectTrigger : MonoBehaviour
    {
        [SerializeField] private RoadObject _roadObject;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Vehicle>())
            {
                _roadObject.MoveRoadToEnd();
            }
        }
    }
}