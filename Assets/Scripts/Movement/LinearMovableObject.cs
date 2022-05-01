using UnityEngine;

namespace Asteroids.Movement
{
    public class LinearMovableObject : MovableObjectBase
    {
        public Vector2 Velocity { get; set; }

        protected override IMoveDriver CreateMoveDriver() => new LinearMoveDriver(transform, Velocity);
    }
}