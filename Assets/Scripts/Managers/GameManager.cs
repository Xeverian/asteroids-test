using Asteroids.Ui;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private InputActionReference _restartAction;
        
        [Space]
        [SerializeField] private EnemySpawnManager _enemySpawnManager;
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private GameEndUi _gameEndUi;
        [SerializeField] private ShipHUDUi _shipHudUi;
        [SerializeField] private Player.Player _player;
        
        private void Start()
        {
            StopGame();
            StartGame();
        }

        private void StartGame()
        {
            _scoreManager.StartCount();
            _enemySpawnManager.StartSpawn();
            
            _player.Respawn();
            _player.gameObject.SetActive(true);
            _player.Killed += OnPlayerKilled;
            
            _gameEndUi.SetActive(false);
            
            _shipHudUi.SetActive(true);
            
            _restartAction.action.started -= OnRestartActionStarted;
        }

        private void StopGame()
        {
            _scoreManager.StopCount();
            _enemySpawnManager.Clear();
            
            _player.Killed -= OnPlayerKilled;
            _player.gameObject.SetActive(false);

            _gameEndUi.Score = _scoreManager.Score;
            _gameEndUi.SetActive(true);
            
            _shipHudUi.SetActive(false);
            
            _restartAction.action.started += OnRestartActionStarted;
        }
        
        private void OnPlayerKilled()
        {
            StopGame();
        }

        private void OnRestartActionStarted(InputAction.CallbackContext context)
        {
            StartGame();
        }
    }
}