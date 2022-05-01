using UnityEngine;

namespace Asteroids.Destruction
{
    public delegate void ObjectDestroyDelegate(Vector2 destroyPosition, bool destroyedCompletely);

    public interface IDestructible
    {
        public event ObjectDestroyDelegate Destroyed;
        public void Destroy(bool completely);
    }
}