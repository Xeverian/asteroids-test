using UnityEngine;

namespace Asteroids.Movement
{
    public class FollowMoveDriver : IMoveDriver
    {
        public Transform Transform { get; }
        public Vector2 Velocity { get; private set; }

        private readonly float _speed;
        private readonly Transform _target;
        private readonly IPlayZoneBounds _bounds;
        private readonly float _stopDistance;

        private readonly Vector3[] _targetPositionOffsets = new Vector3[5];

        public FollowMoveDriver(Transform transform, Transform target, IPlayZoneBounds bounds, float speed, float stopDistance = 1e-5f)
        {
            Transform = transform;
            _target = target;
            _bounds = bounds;
            _speed = speed;

            _stopDistance = stopDistance;
        }
        
        public void Update(float deltaTime)
        {
            Vector2 offset = GetClosestTargetPosition() - Transform.position;

            if (offset.magnitude <= _stopDistance)
            {
                Velocity = Vector2.zero;
                return;
            }
            
            Velocity = _speed * offset.normalized;
            Transform.position += deltaTime * (Vector3) Velocity;
        }

        private Vector3 GetClosestTargetPosition()
        {
            Vector3 closestPosition = _target.position;
            
            float minDistance = Mathf.Infinity;
            
            RefreshTargetPositionOffsets();

            for (int i = 0; i < _targetPositionOffsets.Length; i++)
            {
                Vector3 targetPosition = _target.position + _targetPositionOffsets[i];
                float distance = Vector3.Distance(Transform.position, targetPosition);

                if (distance >= minDistance)
                {
                    continue;
                }

                minDistance = distance;
                closestPosition = targetPosition;
            }

            return closestPosition;
        }

        private void RefreshTargetPositionOffsets()
        {
            var bounds = _bounds.ScreenBounds;

            _targetPositionOffsets[0] = Vector3.zero;
            _targetPositionOffsets[1] = bounds.Width * Vector3.right;
            _targetPositionOffsets[2] = -bounds.Width * Vector3.right;
            _targetPositionOffsets[3] = bounds.Height * Vector3.up;
            _targetPositionOffsets[4] = -bounds.Height * Vector3.up;
        }
    }
}