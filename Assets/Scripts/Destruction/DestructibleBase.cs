using UnityEngine;

namespace Asteroids.Destruction
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DestructibleBase : MonoBehaviour, IDestructible
    {
        public event ObjectDestroyDelegate Destroyed;

        private IDestructor _destructor;

        public void Destroy(bool completely = false)
        {
            Destroyed?.Invoke(transform.position, completely);

            _destructor.Destroy();
        }
        
        protected void Awake()
        {
            _destructor = GetDestructor();
        }

        protected virtual IDestructor GetDestructor() => new SimpleDestructor(gameObject);

        private void OnTriggerEnter2D(Collider2D other)
        {
            Destroy();
        }
    }
}