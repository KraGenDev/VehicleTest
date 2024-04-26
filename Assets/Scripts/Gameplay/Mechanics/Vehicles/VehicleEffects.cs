using Enums;
using UnityEngine;

namespace Gameplay.Mechanics.Vehicles
{
    public class VehicleEffects : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _deadEffect;
        [SerializeField] private ParticleSystem _engineEffect;

        private void OnEnable()
        {
            EventBus.Instance.Subscribe(EventBusAction.GameStart,OnGameStart);
            EventBus.Instance.Subscribe(EventBusAction.Lose,OnDead);
        }

        private void OnDisable()
        {
            EventBus.Instance.Unsubscribe(EventBusAction.GameStart,OnGameStart);
            EventBus.Instance.Unsubscribe(EventBusAction.Lose,OnDead);
        }

        private void OnGameStart() => _engineEffect.Play();

        private void OnDead()
        {
            _engineEffect.Stop();
            
            _deadEffect.transform.SetParent(null);
            _deadEffect.Play();
            
            EventBus.Instance.Unsubscribe(EventBusAction.GameStart,OnGameStart);
            EventBus.Instance.Unsubscribe(EventBusAction.Lose,OnDead);
        }
    }
}