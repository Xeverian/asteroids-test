using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Destruction;
using Asteroids.Movement;
using Asteroids.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Spawn
{
    public abstract class PeriodicSpawnerBase : MonoBehaviour, IPeriodicSpawner
    {
        public event Action<IDestructible> ObjectDestroyed;
        
        [Header("Spawn")]
        [SerializeField] private float _minSpawnPeriod;
        [SerializeField] private float _maxSpawnPeriod;
        [SerializeField] protected Transform _player;
        [SerializeField] private float _playerSafeRadius;

        private IPlayZoneBounds _bounds;
        private Coroutine _spawnRoutine;
        
        private List<IDestructible> _spawnedObjects = new List<IDestructible>();
        
        public void StartSpawn()
        {
            StopSpawn();
            _spawnRoutine = StartCoroutine(PeriodicSpawnRoutine());
        }

        public void StopSpawn()
        {
            if (_spawnRoutine != null)
            {
                StopCoroutine(_spawnRoutine);
            }
        }

        public void Clear()
        {
            var allObjects = _spawnedObjects.ToArray();
            
            foreach (var spawnedObject in allObjects)
            {
                spawnedObject.Destroy(true);
            }
        }

        protected void RemoveFromList(IDestructible destructible)
        {
            ObjectDestroyed?.Invoke(destructible);
            _spawnedObjects.Remove(destructible);
        }
        
        protected void AddToList(IDestructible destructible) => _spawnedObjects.Add(destructible);

        protected Vector2 GetRandomSpawnPosition()
        {
            var bounds = _bounds.ScreenBounds;
            
            Vector2 position;
            do
            {
                position = new Vector2
                {
                    x = Random.Range(bounds.XMin, bounds.XMax),
                    y = Random.Range(bounds.YMin, bounds.YMax),
                };

            } while (Vector2.Distance(position, _player.position) < _playerSafeRadius);

            return position;
        }

        protected abstract void SpawnRandom();
        
        private void Awake()
        {
            _bounds = new CameraPlayZoneBounds(Camera.main);
        }
        
        private void Update()
        {
            _bounds.Update();
        }

        private IEnumerator PeriodicSpawnRoutine()
        {
            while (true)
            {
                float nextSpawnPeriod = Random.Range(_minSpawnPeriod, _maxSpawnPeriod);
                yield return new WaitForSeconds(nextSpawnPeriod);
                SpawnRandom();
            }
        }
    }
}