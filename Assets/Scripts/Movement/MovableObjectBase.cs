using Asteroids.Utils;
using UnityEngine;

namespace Asteroids.Movement
{
    public abstract class MovableObjectBase : MonoBehaviour
    {
        private IMover _mover;
        protected IPlayZoneBounds _bounds;

        protected abstract IMoveDriver CreateMoveDriver();

        protected void Start()
        {
            _bounds = new CameraPlayZoneBounds(Camera.main);

            _mover = new RepeatedSpaceMover(_bounds)
            {
                MoveDriver = CreateMoveDriver()
            };
        }

        protected virtual void Update()
        {
            _mover?.Update(Time.deltaTime);
        }
    }
}