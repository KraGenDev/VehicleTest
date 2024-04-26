using System;
using UnityEngine;

namespace DTO
{
    [Serializable]
    public class AxleInfo 
    {
        [SerializeField] public WheelCollider _leftWheel;
        [SerializeField] public WheelCollider _rightWheel;
        [SerializeField] public bool _motor;
        [SerializeField] public bool _steering;

        public WheelCollider LeftWheel => _leftWheel;
        public WheelCollider RightWheel => _rightWheel;
        public bool Motor => _motor;
        public bool Steering => _steering;
    }
}