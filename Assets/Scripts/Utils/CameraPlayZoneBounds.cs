using Asteroids.Movement;
using UnityEngine;

namespace Asteroids.Utils
{
    public class CameraPlayZoneBounds : IPlayZoneBounds
    {
        private readonly Camera _camera;
        
        public ScreenBounds ScreenBounds { get; private set; }

        public CameraPlayZoneBounds(Camera camera)
        {
            _camera = camera;
            Update();
        }

        public void Update()
        {
            Vector2 lowerLeft = _camera.ViewportToWorldPoint(Vector2.zero);
            Vector2 upperRight = _camera.ViewportToWorldPoint(Vector2.one);

            ScreenBounds = new ScreenBounds
            {
                XMin = lowerLeft.x,
                XMax = upperRight.x,
                YMin = lowerLeft.y,
                YMax = upperRight.y
            };
        }
    }
}