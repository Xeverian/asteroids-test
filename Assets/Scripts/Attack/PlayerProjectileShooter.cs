using Asteroids.Input;
using UnityEngine;

namespace Asteroids.Attack
{
    public class PlayerProjectileShooter : MonoBehaviour
    {
        [Header("Common")]
        [SerializeField] private ShipPlayerInput _input;
        [SerializeField] private Transform _weaponTransform;
        
        [Header("Projectile")]
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private float _projectileCooldown;

        private ProjectileWeapon _projectileWeapon;

        private void Awake()
        {
            _projectileWeapon = new ProjectileWeapon(_weaponTransform, _projectilePrefab, _projectileSpeed, _projectileCooldown);
        }

        private void OnEnable()
        {
            _input.ProjectileFired += OnInputProjectileFired;
        }

        private void OnDisable()
        {
            _input.ProjectileFired -= OnInputProjectileFired;
            _projectileWeapon.Clear();
        }

        private void OnInputProjectileFired()
        {
            _projectileWeapon.Fire();
        }
    }
}