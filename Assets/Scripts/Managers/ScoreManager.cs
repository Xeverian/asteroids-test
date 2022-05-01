using Asteroids.Destruction;
using UnityEngine;

namespace Asteroids.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public int Score { get; private set; }
        
        [SerializeField] private EnemySpawnManager _enemySpawnManager;

        public void StartCount()
        {
            Score = 0;
            _enemySpawnManager.ObjectDestroyed += OnObjectDestroyed;
        }

        public void StopCount()
        {
            _enemySpawnManager.ObjectDestroyed -= OnObjectDestroyed;
        }

        private void OnObjectDestroyed(IDestructible destructible)
        {
            Score++;
        }
    }
}