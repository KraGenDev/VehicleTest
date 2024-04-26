using System;
using Enums;
using UnityEngine;

namespace Gameplay.Mechanics.Vehicles
{
    public class VehicleHealth : MonoBehaviour
    {
        [SerializeField] private int _health;

        private int _currentHealth;
        
        public event Action Damaged;
        public event Action Killed;

        public int MaxHealth => _health;
        public int Health => _currentHealth;
        
        private void OnEnable()
        {
            _currentHealth = _health;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            Damaged?.Invoke();
            
            if(_currentHealth <= 0)
                Die();
        }

        private void Die()
        {
            Killed?.Invoke();  
            EventBus.Instance.Emit(EventBusAction.Lose);
        }
    }
}