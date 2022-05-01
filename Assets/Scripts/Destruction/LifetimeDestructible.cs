using UnityEngine;

namespace Asteroids.Destruction
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class LifetimeDestructible : DestructibleBase
    {
        [SerializeField] private float _lifeTime;

        private float _spawnTime;

        private void OnEnable()
        {
            _spawnTime = Time.time;
        }

        private void Update()
        {
            if (_lifeTime >= 0 && Time.time - _spawnTime > _lifeTime)
            {
                Destroy();
            }
        }
    }
}