using System;
using Interfaces;
using UnityEngine;

namespace Systems.Input
{
    public class MobileInput : MonoBehaviour,IInput
    {
        [SerializeField] private Joystick _joystick;
        
        public event Action<Vector2> Axis;

        private void Update()
        {
            var inputAxis = Vector2.zero;

            inputAxis.x = _joystick.Horizontal;
            inputAxis.y = ApplicationController.Instance.IsStarted ? 1 : 0;
            
            Axis?.Invoke(inputAxis);
        }
    }
}