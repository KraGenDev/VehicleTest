using UnityEngine;

namespace Gameplay.Mechanics.Weapon.Weapons
{
    public class BulletTurret : Weapon
    {
        [SerializeField] private float _throwForce = 35;
        
        protected override void Fire()
        {
            var bullet = _bulletPool.GetFreeElement();
            bullet.gameObject.SetActive(false);
            bullet.transform.position = _firePoint.position;
            bullet.transform.rotation = _firePoint.rotation;
            bullet.gameObject.SetActive(true);
            var throwDirection = _firePoint.forward;
            
            bullet.Throw(throwDirection * _throwForce);
            
            _currentAmmoCount--;
            CallFiredEvent();
        }
    }
}