using DG.Tweening;
using Enums;
using UnityEngine;

namespace UI.Windows
{
    public class StartGameWindow : Window
    {
        [SerializeField] private Vector3 _maxSizeOfTextInAnimation = new Vector3(1.25f,1.25f,1.25f);
        [SerializeField] private float _animationDuration = 1;
        [SerializeField] private GameObject _startText;


        protected override void Start()
        {
            base.Start();
            StartTextAnimation();
        }

        private void StartTextAnimation()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_startText.transform.DOScale(_maxSizeOfTextInAnimation, _animationDuration/2));
            sequence.Append(_startText.transform.DOScale(Vector3.one, _animationDuration/2));
            sequence.SetLoops(-1);
        }


        public void StartGame()
        {
            EventBus.Instance.Emit(EventBusAction.GameStart);
        }
    }
}