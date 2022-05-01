using Asteroids.Input;
using UnityEngine;

namespace Asteroids.Movement
{
    public class PlayerShipMovableObject : MovableObjectBase
    {
        [SerializeField] private ShipPlayerInput _input;
        
        [Space]
        [SerializeField] private float _accelerationRate;
        [SerializeField] private float _rotationRate;
        [SerializeField] private float _maxSpeed;

        private PlayerInputMoveDriver _driver;

        public float CurrentSpeed => _driver.Velocity.magnitude;
        
        protected override IMoveDriver CreateMoveDriver()
        {
            return _driver = new PlayerInputMoveDriver(transform, _input);
        }

        protected override void Update()
        {
            _driver.AccelerationRate = _accelerationRate;
            _driver.RotationRate = _rotationRate;
            _driver.MaxSpeed = _maxSpeed;
            
            base.Update();
        }

        public void Stop()
        {
            _driver?.Stop();
        }
    }
}