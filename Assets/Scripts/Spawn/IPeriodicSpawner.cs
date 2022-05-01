using System;
using Asteroids.Destruction;

namespace Asteroids.Spawn
{
    public interface IPeriodicSpawner
    {
        event Action<IDestructible> ObjectDestroyed; 
        
        void StartSpawn();
        void StopSpawn();

        void Clear();
    }
}