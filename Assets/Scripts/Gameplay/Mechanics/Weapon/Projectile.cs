using Interfaces;
using UnityEngine;

namespace Gameplay.Mechanics.Weapon
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] private int _damage;

        protected virtual void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
            }
            
            gameObject.SetActive(false);
        }
    }
}