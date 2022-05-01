using Asteroids.Destruction;
using Asteroids.Movement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Spawn
{
    public class AsteroidSpawner : PeriodicSpawnerBase
    {
        [Header("Asteroid")]
        [SerializeField] private AsteroidData[] _asteroidDatas;
        
        protected override void SpawnRandom()
        {
            int id = Random.Range(0, _asteroidDatas.Length);
            Spawn(_asteroidDatas[id], GetRandomSpawnPosition());
        }

        private void SpawnOnDestroy(Vector2 position, AsteroidData data, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Spawn(data, position);
            }
        }
        
        private void Spawn(AsteroidData data, Vector2 position)
        {
            var asteroidObject = Instantiate(data.AsteroidPrefab, position, Utils.Utils.GetRandomRotation2D());
            var movableObject = asteroidObject.GetComponent<LinearMovableObject>();
            
            Vector2 direction = Utils.Utils.GetRandomRotation2D() * Vector2.up;
            movableObject.Velocity = data.Speed * direction;

            var spawnedData = data.OnDestroySpawnData;
            int spawnedCount = data.OnDestroySpawnCount;
            
            var destructible = asteroidObject.GetComponent<IDestructible>();
            AddToList(destructible);

            bool needsSpawnOnDestroy = spawnedData && spawnedCount > 0;

            destructible.Destroyed += (destroyPosition, completely) =>
            {
                RemoveFromList(destructible);
                
                if(needsSpawnOnDestroy && !completely)
                {
                    SpawnOnDestroy(destroyPosition, spawnedData, spawnedCount);
                }
            };
        }
    }
}