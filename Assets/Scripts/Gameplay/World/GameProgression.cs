using System;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.World
{
    public class GameProgression : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _targetDistance;
        [SerializeField] private Slider _progressBar;
        [SerializeField] private TextMeshProUGUI _progressText;

        private bool _win = false;


        private void Start()
        {
            _progressBar.maxValue = _targetDistance;
        }

        private void Update()
        {
            CalculateProgress();
        }

        private void CalculateProgress()
        {
            var gameStart = ApplicationController.Instance.IsStarted;

            if (!gameStart) 
                return;

            var currentPosition = _player.position.z;
            _progressBar.value = currentPosition;
            _progressText.text = ((int)currentPosition) + "m";
            if(_progressBar.maxValue - currentPosition <= 0.1f)
                Win();
        }

        private void Win()
        {
            EventBus.Instance.Emit(EventBusAction.Win);
        }
    }
}