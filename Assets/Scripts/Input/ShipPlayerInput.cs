using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Input
{
    public class ShipPlayerInput : MonoBehaviour, IPlayerMoveInput, IPlayerAttackInput
    {
        public event Action ProjectileFired;
        public event Action LaserFired;
        
        [SerializeField] private InputActionAsset _actionAsset;
        
        [Space]
        [SerializeField] private InputActionReference _accelerationAction;
        [SerializeField] private InputActionReference _rotationAction;
        [SerializeField] private InputActionReference _projectileFireAction;
        [SerializeField] private InputActionReference _laserFireAction;

        public float Acceleration { get; private set; }
        public float Rotation { get; private set; }
        
        private void Awake()
        {
            if (_actionAsset)
            {
                _actionAsset.Enable();
            }
        }

        private void OnEnable()
        {
            _accelerationAction.action.performed += OnAccelerationActionPerformed;
            _accelerationAction.action.canceled += OnAccelerationActionCancelled;
            
            _rotationAction.action.performed += OnRotationActionPerformed;
            _rotationAction.action.canceled += OnRotationActionCancelled;
            
            _projectileFireAction.action.started += OnProjectileFireActionStarted;
            _laserFireAction.action.started += OnLaserFireActionStarted;
        }

        private void OnDisable()
        {
            _accelerationAction.action.performed -= OnAccelerationActionPerformed;
            _accelerationAction.action.canceled -= OnAccelerationActionCancelled;
            
            _rotationAction.action.performed -= OnRotationActionPerformed;
            _rotationAction.action.canceled -= OnRotationActionCancelled;
            
            _projectileFireAction.action.started -= OnProjectileFireActionStarted;
            _laserFireAction.action.started -= OnLaserFireActionStarted;
        }

        private void OnAccelerationActionPerformed(InputAction.CallbackContext context)
        {
            Acceleration = context.ReadValue<float>();
        }
        
        private void OnAccelerationActionCancelled(InputAction.CallbackContext context)
        {
            Acceleration = 0;
        }
        
        private void OnRotationActionPerformed(InputAction.CallbackContext context)
        {
            Rotation = context.ReadValue<float>();
        }
        
        private void OnRotationActionCancelled(InputAction.CallbackContext context)
        {
            Rotation = 0;
        }
        
        private void OnLaserFireActionStarted (InputAction.CallbackContext context)
        {
            LaserFired?.Invoke();
        }

        private void OnProjectileFireActionStarted(InputAction.CallbackContext context)
        {
            ProjectileFired?.Invoke();
        }
    }
}