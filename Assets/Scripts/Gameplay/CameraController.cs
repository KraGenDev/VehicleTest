using System;
using DG.Tweening;
using Enums;
using UnityEngine;

namespace Gameplay
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _followSpeed;
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _player;
        [SerializeField] private Vector3 _additionalSpace;
        [SerializeField] private Vector3 _playingCameraAngle;
        [SerializeField] private float _rotateSpeed = 1;


        private float _currentFollowSpeed = 10;
        
        
        private void OnEnable()
        {
            EventBus.Instance.Subscribe(EventBusAction.GameStart,RotateCamera);
        }

        private void OnDisable()
        {
            EventBus.Instance.Unsubscribe(EventBusAction.GameStart,RotateCamera);
        }

        private void Update()
        {
            FollowToPlayer();
        }

        private void RotateCamera()
        {
            _camera.transform.DORotate(_playingCameraAngle, _rotateSpeed).OnComplete((() => _currentFollowSpeed = _followSpeed));
        }

        private void FollowToPlayer()
        {
            var gameStarted = ApplicationController.Instance.IsStarted;
            if (!gameStarted) 
                return;

            var targetPosition = _player.position + _additionalSpace;
            targetPosition.x = 0;
            _camera.transform.position = Vector3.Lerp(_camera.transform.position,targetPosition, _currentFollowSpeed * Time.deltaTime);
        }
    }
}