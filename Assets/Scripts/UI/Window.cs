using DG.Tweening;
using UnityEngine;

namespace UI
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] protected GameObject _window;
        [SerializeField] protected bool _hideOnStart = false;

        [Header("Animations (For CanvasGroup)")] 
        [SerializeField] protected float _showAnimationDelay = 0f;
        [SerializeField] protected float _showAnimationDuration = 0.25f;

        protected bool _isCanvasGroup = false;
        protected CanvasGroup _canvasGroup;
        
        public bool Active { get; private set; } = false;


        protected virtual void Start()
        {
            _isCanvasGroup = _window.TryGetComponent(out _canvasGroup);
            if(_hideOnStart)
                Hide(true);
        }

        public virtual void Show()
        {
            if (Active) 
                return;

            if (_window != null)
            {
                if (_isCanvasGroup)
                {
                    DOVirtual.DelayedCall(_showAnimationDelay, () =>
                    {
                        _canvasGroup.DOFade(1, _showAnimationDuration);
                        _canvasGroup.blocksRaycasts = true;
                    });
                }
                else
                    _window.SetActive(true);
                
                Active = true;
            }
        }

        public virtual void Hide(bool ignoreIsShown = false)
        {
            if (!Active && !ignoreIsShown) 
                return;
            
            if (_window != null)
            {
                if (_isCanvasGroup)
                {
                    _canvasGroup.DOFade(0, _showAnimationDuration);
                    _canvasGroup.blocksRaycasts = false;
                }
                else
                    _window.SetActive(false);
                
                Active = false;
            }
        }
    }
}