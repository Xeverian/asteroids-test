using UnityEngine;

namespace Asteroids.Movement
{
    public class LinearMoveDriver : IMoveDriver
    {
        public Transform Transform { get; }
        public Vector2 Velocity { get; }

        public LinearMoveDriver(Transform transform, Vector2 velocity)
        {
            Transform = transform;
            Velocity = velocity;
        }
        
        public void Update(float deltaTime)
        {
            Transform.position += deltaTime * (Vector3) Velocity;
        }
    }
}