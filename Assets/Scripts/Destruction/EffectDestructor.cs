using UnityEngine;

namespace Asteroids.Destruction
{
    public class EffectDestructor : IDestructor
    {
        private readonly GameObject _destructedObject;
        private readonly GameObject _effectPrefab;

        public EffectDestructor(GameObject destructedObject, GameObject effectPrefab)
        {
            _destructedObject = destructedObject;
            _effectPrefab = effectPrefab;
        }

        public virtual void Destroy()
        {
            Object.Instantiate(_effectPrefab, _destructedObject.transform.position, Quaternion.identity);
            Object.Destroy(_destructedObject);
        }
    }
}