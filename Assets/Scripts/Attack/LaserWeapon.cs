using System;
using Asteroids.Destruction;
using UnityEngine;

namespace Asteroids.Attack
{
    public class LaserWeapon : ILaserWeapon
    {
        private const int MaxTargets = 50;
        private const float RechargeToCooldownRatio = 0.1f;
        private const float CooldownToShowRatio = 0.15f;
        
        public event Action<LaserWeaponState> StateChanged;
        
        private readonly Transform _weaponTransform;
        private readonly GameObject _laserVisuals;
        
        private readonly int _maxShots;
        private readonly float _rechargePeriod;
        private readonly float _range;
        private readonly float _cooldown;
        private readonly LayerMask _enemyLayerMask;

        private float _lastFireTime = Mathf.NegativeInfinity;
        private int _currentShots;
        private float _currentChargeProgress;

        private RaycastHit2D[] _hits = new RaycastHit2D[MaxTargets];

        private LaserWeaponState _state;

        public LaserWeapon(Transform weaponTransform, GameObject laserVisuals, int maxShots, float rechargePeriod, float range, LayerMask enemyLayerMask)
        {
            _weaponTransform = weaponTransform;
            _laserVisuals = laserVisuals;
            _maxShots = maxShots;
            _rechargePeriod = rechargePeriod;
            _cooldown = RechargeToCooldownRatio * rechargePeriod;
            _range = range;
            _enemyLayerMask = enemyLayerMask;
            
            Clear();
        }
        
        public void Fire()
        {
            if (_currentShots < 1 || Time.time - _lastFireTime < _cooldown)
            {
                return;
            }

            _currentShots--;
            _lastFireTime = Time.time;
            _laserVisuals.SetActive(true);

            int targetCount = Physics2D.RaycastNonAlloc(_weaponTransform.position, _weaponTransform.up, _hits, _range, _enemyLayerMask);

            for (int i = 0; i < targetCount; i++)
            {
                TryHitTarget(_hits[i].collider.gameObject);
            }
        }
        
        public void Update(float deltaTime)
        {
            if (Time.time - _lastFireTime >= CooldownToShowRatio * _cooldown)
            {
                _laserVisuals.SetActive(false);
            }
            
            if(_currentChargeProgress < 1)
            {
                _currentChargeProgress += deltaTime / _rechargePeriod;
            }
            else if(_currentShots < _maxShots)
            {
                _currentChargeProgress -= 1;
                _currentShots = Mathf.Min(_currentShots + 1, _maxShots);
            }
            
            if(_currentShots == _maxShots)
            {
                _currentChargeProgress = 0;
            }

            _state.ChargeProgress = _currentChargeProgress;
            _state.Shots = _currentShots;
            
            StateChanged?.Invoke(_state);
        }

        public void Clear()
        {
            _laserVisuals.SetActive(false);
            _currentShots = 0;
            _currentChargeProgress = 0;
        }

        private void TryHitTarget(GameObject target)
        {
            var destructible = target.GetComponent<IDestructible>();
            destructible?.Destroy(true);
        }
    }
}