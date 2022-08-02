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
        
        private Vector2 _currentVelocity;
        private Transform _currentDestination;
        
        public Vector2 CurrentVelocity => _currentVelocity;
        public Transform CurrentDestination { get => _currentDestination; set => _currentDestination = value; }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _ai = GetComponent<IAstarAI>();
        }

        private void OnEnable()
        {
            _ai.onSearchPath += UpdateDestination;
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
            _ai.onSearchPath -= UpdateDestination;
        }

        private void UpdateDestination()
        {
            if (!_currentDestination) 
                _ai.destination = _currentDestination.position;
        }

        public void SetSpeed(float speed) => _ai.maxSpeed = speed;
    }
}