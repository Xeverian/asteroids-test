using UnityEngine;

namespace Asteroids.Movement
{
    public class UfoMovableObject : MovableObjectBase
    {
        public float Speed { get; set; }
        public Transform Target { get; set; }
        
        protected override IMoveDriver CreateMoveDriver() => new FollowMoveDriver(transform, Target, _bounds, Speed);
    }
}