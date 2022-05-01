using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Ui
{
    public class HelpUi : MonoBehaviour
    {
        [SerializeField] private InputActionReference _toggleHelpAction;

        private void Awake()
        {
            _toggleHelpAction.action.started += OnHelpActionStarted;
        }

        private void OnHelpActionStarted(InputAction.CallbackContext context)
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}