using Gameplay.Mechanics.Vehicles;
using UnityEngine;
using UnityEngine.UI;

namespace UI.DataViews
{
    public class VehicleHealthView : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private VehicleHealth _vehicleHealth;

        private void OnEnable()
        {
            _vehicleHealth.Damaged += OnVehicleDamaged;
            _healthSlider.maxValue = _vehicleHealth.MaxHealth;
            _healthSlider.value = _vehicleHealth.MaxHealth;
        }

        private void OnDisable()
        {
            _vehicleHealth.Damaged -= OnVehicleDamaged;
        }

        private void OnVehicleDamaged()
        {
            _healthSlider.value = _vehicleHealth.Health;
        }
    }
}