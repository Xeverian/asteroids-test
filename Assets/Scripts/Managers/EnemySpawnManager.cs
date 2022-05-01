using System;
using Asteroids.Destruction;
using Asteroids.Spawn;
using UnityEngine;

namespace Asteroids.Managers
{
    public class EnemySpawnManager : MonoBehaviour
    {
        public event Action<IDestructible> ObjectDestroyed;
        
        private IPeriodicSpawner[] _spawners;

        public void StartSpawn()
        {
            foreach (var spawner in _spawners)
            {
                spawner.StartSpawn();
                spawner.ObjectDestroyed += InvokeDestroyed;
            }
        }

        public void Clear()
        {
            foreach (var spawner in _spawners)
            {
                spawner.StopSpawn();
                spawner.Clear();
                
                spawner.ObjectDestroyed -= InvokeDestroyed;
            }
        }

        private void Awake()
        {
            _spawners = GetComponentsInChildren<IPeriodicSpawner>();
        }

        private void InvokeDestroyed(IDestructible destructible)
        {
            ObjectDestroyed?.Invoke(destructible);
        }
    }
}