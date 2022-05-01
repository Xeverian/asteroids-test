using UnityEngine;

namespace Asteroids.Destruction
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EffectDestructible : DestructibleBase
    {
        [SerializeField] private GameObject _destructionEffectPrefab;
        protected override IDestructor GetDestructor() => new EffectDestructor(gameObject, _destructionEffectPrefab);
    }
}