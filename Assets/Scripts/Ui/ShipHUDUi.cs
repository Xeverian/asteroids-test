using Asteroids.Attack;
using Asteroids.Player;
using TMPro;
using UnityEngine;

namespace Asteroids.Ui
{
    public class ShipHUDUi : MonoBehaviour
    {
        [SerializeField] private Player.Player _player;
        [SerializeField] private TextMeshProUGUI _shipText;
        [SerializeField] private TextMeshProUGUI _laserText;
        
        private string _hudMessageText;

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        private void OnEnable()
        {
            _player.ShipStateChanged += OnPlayerShipStateChanged;
            _player.LaserStateChanged += OnPlayerLaserStateChanged;
        }

        private void OnDisable()
        {
            _player.ShipStateChanged -= OnPlayerShipStateChanged;
            _player.LaserStateChanged -= OnPlayerLaserStateChanged;
        }

        private void OnPlayerShipStateChanged(PlayerShipState state)
        {
            _shipText.text = $"<b>Ship:</b>\n" +
                             $"X: {state.Position.x:F1}\n" +
                             $"Y: {state.Position.y:F1}\n" +
                             $"A: {state.RotationAngle:F0}\n" +
                             $"S: {state.Speed:F1} m/s";
        }
        
        private void OnPlayerLaserStateChanged(LaserWeaponState state)
        {
            _laserText.text = $"<b>Laser:</b>\n" +
                              $"Shots: {state.Shots}\n" +
                              $"Charge: {100 * state.ChargeProgress:F0} %\n";
        }
    }
}