using Asteroids.Input;
using UnityEngine;

namespace Asteroids.Movement
{
    public class PlayerInputMoveDriver : IMoveDriver
    {
        public Transform Transform { get; }
        public Vector2 Velocity { get; private set; }

        public float AccelerationRate { get; set; }
        public float RotationRate { get; set; }
        public float MaxSpeed { get; set; }

        private readonly IPlayerMoveInput _input;

        public PlayerInputMoveDriver(Transform transform, IPlayerMoveInput input)
        {
            Transform = transform;
            _input = input;
        }
        
        public void Update(float deltaTime)
        {
            float rotationAngle = deltaTime * RotationRate * _input.Rotation;

            Transform.Rotate(Vector3.back, rotationAngle);

            Vector2 acceleration = AccelerationRate * _input.Acceleration * Transform.up;
            Velocity += acceleration * deltaTime;

            if (Velocity.magnitude > MaxSpeed)
            {
                Velocity = MaxSpeed * Velocity.normalized;
            }
            
            Transform.position += deltaTime * (Vector3) Velocity;
        }

        public void Stop()
        {
            Velocity = Vector2.zero;
        }
    }
}