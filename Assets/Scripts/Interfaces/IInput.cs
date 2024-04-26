using System;
using UnityEngine;

namespace Interfaces
{
    public interface IInput
    {
        public event Action<Vector2> Axis;
    }
}