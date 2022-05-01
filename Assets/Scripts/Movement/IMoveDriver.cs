using UnityEngine;

namespace Asteroids.Movement
{
    public interface IMoveDriver
    {
        public Transform Transform { get; }
        public Vector2 Velocity { get; }
        
        void Update(float deltaTime);
    }
}