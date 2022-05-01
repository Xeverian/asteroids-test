using System;

namespace Asteroids.Attack
{
    public struct LaserWeaponState
    {
        public int Shots;
        public float ChargeProgress;
    }
    
    public interface ILaserWeapon : IWeapon
    {
        event Action<LaserWeaponState> StateChanged;
        void Update(float deltaTime);
    }
}