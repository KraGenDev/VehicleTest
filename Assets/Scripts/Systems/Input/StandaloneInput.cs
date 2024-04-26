using System;
using Interfaces;
using UnityEngine;

namespace Systems.Input
{
    public class StandaloneInput : MonoBehaviour, IInput
    {
        public event Action<Vector2> Axis;

        private bool _zeroInputSend = false;

        private void Update()
        {
            var input = Vector2.zero;
            input.x = UnityEngine.Input.GetAxis("Horizontal");
            input.y = UnityEngine.Input.GetAxis("Vertical");
        
            Axis?.Invoke(input);
        }
    }
}