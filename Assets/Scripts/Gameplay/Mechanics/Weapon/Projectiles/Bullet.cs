using UnityEngine;

namespace Gameplay.Mechanics.Weapon.Projectiles
{
    public class Bullet : Projectile
    {
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody ??= GetComponent<Rigidbody>();
        }

        public void Throw(Vector3 direction)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(direction,ForceMode.Impulse);
        }
    }
}