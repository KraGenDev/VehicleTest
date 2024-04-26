using UnityEngine;

namespace Gameplay.Mechanics.Enemy
{
    public class EnemyEffects : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private ParticleSystem _deadEffect;

        private void OnEnable()
        {
            if (_enemyHealth != null)
                _enemyHealth.Dead += OnDead;
        }

        private void OnDisable()
        {
            if (_enemyHealth != null)
                _enemyHealth.Dead -= OnDead;
        }

        private void OnDead()
        {
            _deadEffect.transform.SetParent(null);
            _deadEffect.transform.position = transform.position;
            _deadEffect.Play();
        }
    }
}