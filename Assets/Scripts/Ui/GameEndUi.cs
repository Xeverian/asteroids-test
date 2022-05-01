using TMPro;
using UnityEngine;

namespace Asteroids.Ui
{
    public class GameEndUi : MonoBehaviour
    {
        private const string ScoreMarker = "<score>";
        
        [SerializeField] private TextMeshProUGUI _gameEndMessage;
        private string _gameEndMessageText;

        public int Score { get; set; }
        
        public void SetActive(bool value)
        {
            gameObject.SetActive(value);

            if (value)
            {
                _gameEndMessage.text = _gameEndMessageText.Replace(ScoreMarker, Score.ToString());
            }
        }

        private void Awake()
        {
            _gameEndMessageText = _gameEndMessage.text;
        }
    }
}