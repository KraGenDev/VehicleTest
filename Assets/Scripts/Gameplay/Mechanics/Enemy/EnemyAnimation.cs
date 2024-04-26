using System.Linq;
using DTO;
using Enums;
using UnityEngine;

namespace Gameplay.Mechanics.Enemy
{
    public class EnemyAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private EnemyAnimationNameByType[] _animationNameByTypes;

        private EnemyAnimationType _currentAnimation;

        
        public void PlayAnimation(EnemyAnimationType type)
        {
            if (_currentAnimation == type) 
                return;
            
            var animation = _animationNameByTypes.FirstOrDefault(item => item.Type == type);

            if (animation == null)
            {
                Debug.LogWarning($"No Animation with type {type}");
                return;
            }

            _currentAnimation = type;
            _animator.Play(animation.Name);
        }
    }
}