using System;
using Enums;

namespace DTO
{
    [Serializable]
    public record EnemyAnimationNameByType
    {
        public EnemyAnimationType Type;
        public string Name;
    }
}