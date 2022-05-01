namespace Asteroids.Movement
{
    public interface IMover
    {
        IMoveDriver MoveDriver { get; set; }
        
        void Update(float deltaTime);
    }
}