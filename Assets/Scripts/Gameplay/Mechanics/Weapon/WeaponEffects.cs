using System;
using UnityEngine;

namespace Gameplay.Mechanics.Weapon
{
    public class WeaponEffects : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private ParticleSystem _fireEffect;

        private void OnEnable()
        {
            if(_weapon != null)
                _weapon.Fired += OnFired;
        }

        private void OnDisable()
        {
            if(_weapon != null)
                _weapon.Fired -= OnFired;
        }

        private void OnFired()
        {
            _fireEffect.Play();
        }
    }
}