using System;
using System.Collections;
using Enums;
using Gameplay.Mechanics.Weapon.Projectiles;
using UnityEngine;

namespace Gameplay.Mechanics.Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected int _ammoCount;
        [SerializeField] private float _reloadDuration;
        [SerializeField] private float _useDelay = 0.5f;
        [SerializeField] private bool _canFire = false;
        [SerializeField] protected Transform _firePoint;
        [SerializeField] protected Bullet _bullet;

        private float _timer = 0;
        protected int _currentAmmoCount = 0;
        protected PoolMono<Bullet> _bulletPool;

        public event Action Fired;
        

        private void Awake()
        {
            var bulletCount = 15;
            _bulletPool = new PoolMono<Bullet>(_bullet, bulletCount);
            _bulletPool.AutoExpand = true;
            _currentAmmoCount = _ammoCount;
        }

        private void OnEnable()
        {
            EventBus.Instance.Subscribe(EventBusAction.GameStart,Activate);
            EventBus.Instance.Subscribe(EventBusAction.Lose,Deactivate);
            EventBus.Instance.Subscribe(EventBusAction.Win,Deactivate);
        }

        private void OnDisable()
        {
            EventBus.Instance.Unsubscribe(EventBusAction.GameStart,Activate);
            EventBus.Instance.Unsubscribe(EventBusAction.Lose,Deactivate);
            EventBus.Instance.Unsubscribe(EventBusAction.Win,Deactivate);
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _useDelay)
            {
                _timer = 0;
                
                Use();
            }
        }

        public void Activate() => _canFire = true;
        
        public void Deactivate() => _canFire = false;
        
        protected virtual void Use()
        {
            if (!_canFire) 
                return;
            
            if (_currentAmmoCount > 0)
            {
                Fire();
            }
            else
            {
                Reload();
            }
        }
        
        protected virtual void Fire()
        {
            var bullet = _bulletPool.GetFreeElement();
            bullet.gameObject.SetActive(false);
            bullet.transform.position = _firePoint.position;
            bullet.transform.rotation = _firePoint.rotation;
            bullet.gameObject.SetActive(true);
            bullet.Throw(_firePoint.forward);
            
            _currentAmmoCount--;
            CallFiredEvent();
        }
        
        protected void CallFiredEvent() => Fired?.Invoke();

        protected virtual void Reload()
        {
            _canFire = false;
            StartCoroutine(ReloadCoroutine());
        }

        private IEnumerator ReloadCoroutine()
        {
            yield return new WaitForSeconds(_reloadDuration);
            _currentAmmoCount = _ammoCount;
            _canFire = true;
        }
    }
}