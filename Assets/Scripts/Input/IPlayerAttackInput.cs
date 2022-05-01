using System;

namespace Asteroids.Input
{
    public interface IPlayerAttackInput
    {
        public event Action ProjectileFired;
        public event Action LaserFired;
    }
}