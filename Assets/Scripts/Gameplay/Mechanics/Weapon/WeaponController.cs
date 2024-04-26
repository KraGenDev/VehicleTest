using Interfaces;
using UnityEngine;
using Zenject;

namespace Gameplay.Mechanics.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private float _maxHorizontalAngle;
        [SerializeField] private float _rotationSpeed;

        [Inject] private IInput _input;

        private void Awake()
        {
            if(_input != null)
                _input.Axis += RotateWeapon;
        }

        private void OnDisable()
        {
            if(_input != null)
                _input.Axis -= RotateWeapon;
        }

        private void RotateWeapon(Vector2 input)
        {
            var horizontalAxis = input.x;
            var weaponRotation = _weapon.transform.rotation.eulerAngles;
            var normalizedWeaponRotationY = NormalizeAngle(weaponRotation.y);
            var targetHorizontalRotation = normalizedWeaponRotationY + horizontalAxis * _rotationSpeed * Time.fixedDeltaTime;

            targetHorizontalRotation = Mathf.Clamp(targetHorizontalRotation, -_maxHorizontalAngle, _maxHorizontalAngle);

            var smoothedRotation = Mathf.MoveTowardsAngle(normalizedWeaponRotationY, targetHorizontalRotation, _rotationSpeed * Time.fixedDeltaTime);
            _weapon.transform.rotation = Quaternion.Euler(weaponRotation.x, smoothedRotation, weaponRotation.z);
        }

        private float NormalizeAngle(float angle)
        {
            if (angle > 180f)
            {
                return angle - 360f;
            }
            return angle;
        }
    }
}