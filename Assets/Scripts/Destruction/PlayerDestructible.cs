using UnityEngine;

namespace Asteroids.Destruction
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerDestructible : DestructibleBase
    {
        protected override IDestructor GetDestructor() => new NullDestructor();
    }
}