using System;
using UnityEngine;
using Pathfinding;

namespace Core
{
    [RequireComponent(typeof(AIPath))]
    public class AIMovement: CoreComponent
    {
        private Rigidbody2D _rb;
        private IAstarAI _ai;
        private AIDestinationSetter _destinationSetter;
        
        private Vector2 _currentVelocity;
        private Transform _currentDestination;
        
        public Vector2 CurrentVelocity => _currentVelocity;
        public Transform CurrentDestination { get => _currentDestination; set => _currentDestination = value; }

        private void Awake()
        {
            _rb = GetComponentInParent<Rigidbody2D>();
            _ai = GetComponent<IAstarAI>();
            _destinationSetter = GetComponent<AIDestinationSetter>();
        }

        private void OnEnable()
        {
            Debug.Log(_currentDestination);
            // _ai.onSearchPath += UpdateDestination;
        }

        private void Start()
        {
            _currentDestination = transform;
        }

        internal void LogicUpdate()
        {
            _currentVelocity = _rb.velocity;
        }
        
        private void OnDisable()
        {
           // _ai.onSearchPath -= UpdateDestination;
        }

        private void UpdateDestination()
        {
            Debug.Log(_currentDestination);
            if (!_currentDestination) 
                _ai.destination = _currentDestination.position;
        }

        private void FixedUpdate()
        {
            _rb.transform.position = _ai.position;
        }

        private void Update()
        {
            _destinationSetter.target = _currentDestination;
        }

        public void SetSpeed(float speed) => _ai.maxSpeed = speed;
    }
}