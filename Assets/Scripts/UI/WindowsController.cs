using System.Linq;
using DTO;
using Enums;
using UnityEngine;

namespace UI
{
    public class WindowsController : MonoBehaviour
    {
        [SerializeField] private WindowsByType[] _windowsByTypes;

        private WindowType _currentWindow = WindowType.None;


        private void Start()
        {
            ShowWindowByType(WindowType.StartWindow);
            
            EventBus.Instance.Subscribe(EventBusAction.GameStart,ShowGameUI);
            EventBus.Instance.Subscribe(EventBusAction.Win,ShowWinWindow);
            EventBus.Instance.Subscribe(EventBusAction.Lose,ShowLoseWindow);
        }

        private void OnDisable()
        {
            EventBus.Instance.Unsubscribe(EventBusAction.GameStart,ShowGameUI);
            EventBus.Instance.Unsubscribe(EventBusAction.Win,ShowWinWindow);
            EventBus.Instance.Unsubscribe(EventBusAction.Lose,ShowLoseWindow);
        }

        private void ShowGameUI() => ShowWindowByType(WindowType.GameUI);
        private void ShowWinWindow() => ShowWindowByType(WindowType.WinWindow);
        private void ShowLoseWindow() => ShowWindowByType(WindowType.LoseWindow);

        public void ShowWindowByType(WindowType type)
        {
            if (_currentWindow == type)
                return;

            var requiredWindow = _windowsByTypes.FirstOrDefault(item => item.Type == type);

            if (requiredWindow == null)
            {
                Debug.LogWarning($"Required windows not found: {type}");
                return;
            }

            CloseAllActiveWindows();
            
            requiredWindow.Window.Show();
            _currentWindow = type;
        }

        private void CloseAllActiveWindows()
        {
            var activeWindows = _windowsByTypes
                .Where(item => item.Window.Active)
                .Select(item=> item.Window)
                .ToArray();

            for (int i = 0; i < activeWindows.Length; i++)
            {
                activeWindows[i].Hide();
            }
        }
    }
}