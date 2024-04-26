using System;
using Interfaces;
using UnityEngine;

namespace Gameplay.Mechanics.Enemy
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _health;

        private int _currentHealth;

        public event Action Dead;

        private void OnEnable()
        {
            _currentHealth = _health;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
                Die();
        }

        public void Die()
        {
            Dead?.Invoke();
            gameObject.SetActive(false);
        }
    }
}