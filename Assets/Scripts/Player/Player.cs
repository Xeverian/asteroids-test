using System;
using Asteroids.Attack;
using Asteroids.Destruction;
using Asteroids.Movement;
using UnityEngine;

namespace Asteroids.Player
{
    public struct PlayerShipState
    {
        public Vector2 Position;
        public float RotationAngle;
        public float Speed;
    }

    public class Player : MonoBehaviour, IPlayer
    {
        public event Action Killed;
        public event Action<PlayerShipState> ShipStateChanged;
        public event Action<LaserWeaponState> LaserStateChanged;

        [SerializeField] private PlayerShipMovableObject _movable;
        [SerializeField] private PlayerLaserShooter _laserShooter;
        
        private IDestructible _destructible;
        private Vector2 _initialPosition;
        private Quaternion _initialRotation;
        
        private PlayerShipState _currentState;

        public void Respawn()
        {
            _movable.Stop();
            transform.SetPositionAndRotation(_initialPosition, _initialRotation);
        }
        
        private void Awake()
        {
            _destructible = GetComponent<IDestructible>();
            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
        }

        private void OnEnable()
        {
            _destructible.Destroyed += OnDestroyed;
            _laserShooter.LaserWeapon.StateChanged += InvokeLaserStateChanged;
        }

        private void OnDisable()
        {
            _destructible.Destroyed -= OnDestroyed;
            _laserShooter.LaserWeapon.StateChanged -= InvokeLaserStateChanged;
        }

        private void Update()
        {
            _currentState.Position = transform.position;
            _currentState.Speed = _movable.CurrentSpeed;
            _currentState.RotationAngle = transform.eulerAngles.z;
            
            ShipStateChanged?.Invoke(_currentState);
        }

        private void OnDestroyed(Vector2 destroyPosition, bool completely)
        {
            Killed?.Invoke();
        }

        private void InvokeLaserStateChanged(LaserWeaponState state)
        {
            LaserStateChanged?.Invoke(state);
        }
    }
}