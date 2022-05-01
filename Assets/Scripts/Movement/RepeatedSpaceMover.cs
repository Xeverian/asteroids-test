using UnityEngine;

namespace Asteroids.Movement
{
    public class RepeatedSpaceMover : IMover
    {
        public IMoveDriver MoveDriver { get; set; }
        
        private readonly IPlayZoneBounds _bounds;
        
        public RepeatedSpaceMover(IPlayZoneBounds bounds)
        {
            _bounds = bounds;
        }
        
        public void Update(float deltaTime)
        {
            if (MoveDriver == null)
            {
                return;
            }

            MoveDriver.Update(deltaTime);
            _bounds.Update();
            
            Vector2 position = MoveDriver.Transform.position;
            var bounds = _bounds.ScreenBounds;
            
            if (position.x < bounds.XMin)
            {
                position.x += bounds.Width;
            }
            else if (position.x > bounds.XMax)
            {
                position.x -= bounds.Width;
            }
            
            if (position.y < bounds.YMin)
            {
                position.y += bounds.Height;
            }
            else if (position.y > bounds.YMax)
            {
                position.y -= bounds.Height;
            }

            MoveDriver.Transform.position = position;
        }
    }
}