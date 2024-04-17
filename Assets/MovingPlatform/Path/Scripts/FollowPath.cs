using System.Collections.Generic;
using UnityEngine;


namespace SAS.Waypoints
{
    [RequireComponent(typeof(Rigidbody))]
    public class FollowPath : MonoBehaviour
    {
        enum FollowType
        {
            Lerp,
            MoveTowards
        }

        [SerializeField] Path m_Path = default;
        [SerializeField] private float m_Speed = 1;
        [SerializeField] private float m_MaxDistanceToGoal = 0.1f;
        [SerializeField] private FollowType m_FollowType = default;

        private IEnumerator<Transform> _currentPoint;
        private float _squaredMaxDistanceToGoal => m_MaxDistanceToGoal * m_MaxDistanceToGoal;
        private Vector3 _position;
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;

            if (m_Path == null)
            {
                Debug.LogError($"Path can not be null {gameObject}");
                return;
            }

            _currentPoint = m_Path.GetPathEnumerator();
            _currentPoint.MoveNext();
            _position = transform.position;
        }

        private void Update()
        {
            if (_currentPoint == null || _currentPoint.Current == null)
                return;

            if (m_FollowType == FollowType.MoveTowards)
                _position = Vector3.MoveTowards(_position, _currentPoint.Current.position, Time.deltaTime * m_Speed);
            else
                _position = Vector3.Lerp(_position, _currentPoint.Current.position, Time.deltaTime * m_Speed);

            var distanceSquared = (_position - _currentPoint.Current.position).sqrMagnitude;
            if (distanceSquared < _squaredMaxDistanceToGoal)
                _currentPoint.MoveNext();
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_position);
        }
    }
}
