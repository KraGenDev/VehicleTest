using DG.Tweening;
using Enums;
using Gameplay.Mechanics.Vehicles;
using UnityEngine;

namespace Gameplay.Mechanics.Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected EnemyAnimation _animation;
        [SerializeField] protected EnemyHealth _health;
        [Space]
        [SerializeField] protected float _runSpeed;
        [SerializeField] protected float _rotationSpeed = 0.5f;
        [SerializeField] protected int _damage;

        protected Transform _player;
        protected bool _hasTarget;
        protected Rigidbody _rigidbody;


        protected virtual void Awake()
        {
            _rigidbody ??= GetComponent<Rigidbody>();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<VehicleHealth>())
            {
                _player = other.transform;
                _hasTarget = true;
                _animation?.PlayAnimation(EnemyAnimationType.Run);
            }
        }
        
        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<VehicleHealth>())
            {
                _player = null;
                _hasTarget = false;
                _animation?.PlayAnimation(EnemyAnimationType.Idle);
            }
        }

        protected virtual void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out VehicleHealth vehicleHealth))
            {
                vehicleHealth.TakeDamage(_damage);
                _health.Die();
            }
        }

        protected virtual void FixedUpdate()
        {
            MoveToTarget();
        }

        protected virtual void MoveToTarget()
        {
            if (!_hasTarget) 
                return;

            var lookAtTarget = _player.position;
            lookAtTarget.y = transform.position.y;
            transform.DODynamicLookAt(lookAtTarget, _rotationSpeed);
            
            var direction = (_player.position - transform.position).normalized;
            _rigidbody.velocity = direction * (_runSpeed * Time.fixedDeltaTime);
        }
    }
}