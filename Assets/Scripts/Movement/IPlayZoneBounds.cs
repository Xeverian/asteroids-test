using Asteroids.Utils;

namespace Asteroids.Movement
{
    public interface IPlayZoneBounds
    {
        ScreenBounds ScreenBounds { get; }
        void Update();
    }
}