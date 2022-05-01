using UnityEngine;

namespace Asteroids.Destruction
{
    public class SimpleDestructor : IDestructor
    {
        private readonly GameObject _destructedObject;

        public SimpleDestructor(GameObject destructedObject)
        {
            _destructedObject = destructedObject;
        }

        public void Destroy()
        {
            Object.Destroy(_destructedObject);
        }
    }
}