using Asteroids.Destruction;
using Asteroids.Movement;
using UnityEngine;

namespace Asteroids.Spawn
{
    public class UfoPeriodicSpawner : PeriodicSpawnerBase
    {
        [Header("Ufo")]
        [SerializeField] private GameObject _ufoPrefab;

        [SerializeField] private float _ufoMinSpeed;
        [SerializeField] private float _ufoMaxSpeed;
        
        protected override void SpawnRandom()
        {
            var ufoObject = Instantiate(_ufoPrefab, GetRandomSpawnPosition(), Utils.Utils.GetRandomRotation2D());
            var movableObject = ufoObject.GetComponent<UfoMovableObject>();

            movableObject.Speed = Random.Range(_ufoMinSpeed, _ufoMaxSpeed);
            movableObject.Target = _player;
            
            var destructible = ufoObject.GetComponent<IDestructible>();
            AddToList(destructible);

            destructible.Destroyed += (destroyPosition, completely) => RemoveFromList(destructible);
        }
    }
}