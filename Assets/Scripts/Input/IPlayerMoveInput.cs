namespace Asteroids.Input
{
    public interface IPlayerMoveInput
    {
        float Acceleration { get; }
        float Rotation { get; }
    }
}