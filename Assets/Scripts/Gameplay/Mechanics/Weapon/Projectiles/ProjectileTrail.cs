using UnityEngine;

namespace Gameplay.Mechanics.Weapon.Projectiles
{
    public class ProjectileTrail : MonoBehaviour
    {
        [SerializeField] private TrailRenderer _trail;

        private void OnEnable()
        {
            _trail?.Clear();
        }
    }
}