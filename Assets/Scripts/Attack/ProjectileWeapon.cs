using System.Collections.Generic;
using Asteroids.Destruction;
using Asteroids.Movement;
using UnityEngine;

namespace Asteroids.Attack
{
    public class ProjectileWeapon : IWeapon
    {
        private readonly Transform _weaponTransform;
        private readonly GameObject _projectilePrefab;
        private readonly float _projectileSpeed;
        private readonly float _cooldown;
        
        private readonly List<IDestructible> _spawnedObjects = new List<IDestructible>();

        private float _lastFireTime = Mathf.NegativeInfinity;
        
        public ProjectileWeapon(Transform weaponTransform, GameObject projectilePrefab, float projectileSpeed, float cooldown)
        {
            _weaponTransform = weaponTransform;
            _projectilePrefab = projectilePrefab;
            _projectileSpeed = projectileSpeed;
            _cooldown = cooldown;
        }
        
        public void Fire()
        {
            if (Time.time - _lastFireTime < _cooldown)
            {
                return;
            }

            _lastFireTime = Time.time;
            
            var projectileObject = Object.Instantiate(_projectilePrefab, _weaponTransform.position, _weaponTransform.rotation);
            var movableObject = projectileObject.GetComponent<LinearMovableObject>();
            
            movableObject.Velocity = _projectileSpeed * _weaponTransform.up;
            
            var destructible = projectileObject.GetComponent<IDestructible>();
            _spawnedObjects.Add(destructible);
            
            destructible.Destroyed += (destroyPosition, completely) => _spawnedObjects.Remove(destructible);
        }
        
        public void Clear()
        {
            var allObjects = _spawnedObjects.ToArray();
            
            foreach (var spawnedObject in allObjects)
            {
                spawnedObject.Destroy(true);
            }
            
            _lastFireTime = Mathf.NegativeInfinity;
        }
    }
}