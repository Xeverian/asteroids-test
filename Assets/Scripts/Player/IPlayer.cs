using System;

namespace Asteroids.Player
{
    public interface IPlayer
    {
        public event Action Killed;
        void Respawn();
    }
}