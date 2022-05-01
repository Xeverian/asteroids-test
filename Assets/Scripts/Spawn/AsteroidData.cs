using UnityEngine;

namespace Asteroids.Spawn
{
    [CreateAssetMenu(menuName = "Data/Asteroid", fileName = "AsteroidData")]
    public class AsteroidData : ScriptableObject
    {
        [Header("Main")]
        [SerializeField] private GameObject _asteroidPrefab;
        [SerializeField] private float _speed;

        [Header("On Destroy")]
        [SerializeField] private AsteroidData _onDestroySpawnData;
        [SerializeField] private int _onDestroySpawnCount;

        public GameObject AsteroidPrefab => _asteroidPrefab;
        public float Speed => _speed;

        public AsteroidData OnDestroySpawnData => _onDestroySpawnData;
        public int OnDestroySpawnCount => _onDestroySpawnCount;
    }
}