using System.Collections.Generic;
using DTO;
using Enums;
using Interfaces;
using UnityEngine;
using Zenject;

namespace Gameplay.Mechanics.Vehicles
{
    public abstract class Vehicle : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private List<AxleInfo> axleInfos;
        [SerializeField] private float pressingForce;
        [SerializeField] private float maxMotorTorque;
        [SerializeField] private float maxSteeringAngle;
        [SerializeField] private float boostForce;
        [SerializeField] private float breakForce = 1000;

        [Inject] private IInput _input;
        private Rigidbody _rigidbody;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            if (_input != null)
                _input.Axis += ApplyTorque;
            
            EventBus.Instance.Subscribe(EventBusAction.GameStart,AddBoost);
            EventBus.Instance.Subscribe(EventBusAction.Lose,Break);
            EventBus.Instance.Subscribe(EventBusAction.Win,Break);
        }

        private void OnDisable()
        {
            if(_input != null)
                _input.Axis -= ApplyTorque;
            
            EventBus.Instance.Unsubscribe(EventBusAction.GameStart,AddBoost);
            EventBus.Instance.Unsubscribe(EventBusAction.Lose,Break);
            EventBus.Instance.Unsubscribe(EventBusAction.Win,Break);
        }

        private void AddBoost()
        {
            _rigidbody.AddForce(transform.forward*boostForce,ForceMode.Impulse);
        }

        private void Break()
        {
            foreach (var axleInfo in axleInfos) 
            {
                if (axleInfo.Motor)
                {
                    axleInfo.LeftWheel.brakeTorque = breakForce;
                    axleInfo.RightWheel.brakeTorque = breakForce;
                }
                ApplyLocalPositionToVisuals(axleInfo.LeftWheel);
                ApplyLocalPositionToVisuals(axleInfo.RightWheel);
            }

            _rigidbody.AddForce(Vector3.down * pressingForce);
        }

        private void ApplyLocalPositionToVisuals(WheelCollider collider)
        {
            if (collider.transform.childCount == 0) {
                return;
            }
     
            var visualWheel = collider.transform.GetChild(0);
     
            Vector3 position;
            Quaternion rotation;
            collider.GetWorldPose(out position, out rotation);
     
            visualWheel.transform.position = position;
            visualWheel.transform.rotation = rotation;
        }

        private void ApplyTorque(Vector2 input)
        {
            var motor = maxMotorTorque * input.y;
            var steering = maxSteeringAngle * input.x;
        
            foreach (var axleInfo in axleInfos) 
            {
                if (axleInfo.Steering) 
                {
                    axleInfo.LeftWheel.steerAngle = steering;
                    axleInfo.RightWheel.steerAngle = steering;
                }
                if (axleInfo.Motor) 
                {
                    axleInfo.LeftWheel.motorTorque = motor;
                    axleInfo.RightWheel.motorTorque = motor;
                }
                ApplyLocalPositionToVisuals(axleInfo.LeftWheel);
                ApplyLocalPositionToVisuals(axleInfo.RightWheel);
            }

            _rigidbody.AddForce(Vector3.down * pressingForce);
        }
    }
}