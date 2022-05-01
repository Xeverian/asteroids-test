using Asteroids.Input;
using UnityEngine;

namespace Asteroids.Attack
{
    public class PlayerLaserShooter : MonoBehaviour
    {
        [Header("Common")]
        [SerializeField] private ShipPlayerInput _input;
        [SerializeField] private Transform _weaponTransform;

        [Header("Laser")] 
        [SerializeField] private GameObject _laserVisuals;
        [SerializeField] private int _maxShots;
        [SerializeField] private float _rechargePeriod;
        [SerializeField] private float _range;
        [SerializeField] private LayerMask _enemyLayerMask;
        
        private LaserWeapon _laserWeapon;

        public LaserWeapon LaserWeapon => _laserWeapon;

        private void Awake()
        {
            _laserWeapon = new LaserWeapon(_weaponTransform, _laserVisuals, _maxShots, _rechargePeriod, _range, _enemyLayerMask);
        }

        private void OnEnable()
        {
            _input.LaserFired += OnInputLaserFired;
        }

        private void OnDisable()
        {
            _input.LaserFired -= OnInputLaserFired;
            _laserWeapon.Clear();
        }
        
        private void Update()
        {
            _laserWeapon.Update(Time.deltaTime);
        }
        
        private void OnInputLaserFired()
        {
            _laserWeapon.Fire();
        }
    }
}