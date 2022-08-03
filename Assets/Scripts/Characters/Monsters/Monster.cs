using System.Collections.Generic;
using Base.FSM;
using Core;
using Data;
using Utilities;
using UnityEngine;

namespace Characters.Monsters
{
    public class Monster: Entity
    {
        [SerializeField] private MonsterDataSo _data;
        [SerializeField] private bool _patrol;
        [SerializeField] private List<Transform> patrolPoints;

        private Animator _anim;
        
        internal MonsterStateMachine StateMachine { get; private set; }
        internal GameCore Core { get; private set; }
        internal MonsterDataSo Data => _data;
        internal Collider2D target;
        
        public MonsterIdleState IdleState { get; private set; }
        public MonsterChaseState ChaseState { get; private set; }
        public MonsterPatrolState PatrolState { get; private set; }
        
        public List<Transform> PatrolPoints => patrolPoints;
        public bool Patrol => _patrol;

        private void Awake()
        {
            Core = GetComponentInChildren<GameCore>();
            _anim = GetComponent<Animator>();
            
            StateMachine = new MonsterStateMachine();
            IdleState = new MonsterIdleState(this, "idle");
            ChaseState = new MonsterChaseState(this, "chase");
            PatrolState = new MonsterPatrolState(this, "patrol");
        }

        private void Start()
        {
            StateMachine.Initialize(IdleState);
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }

        private void Update()
        {
            StateMachine.CurrentState.LogicUpdate();
        }

        internal void SetAnimBool(int hash, bool value)
        {
            _anim.SetBool(hash, value);
        }

#if UNITY_EDITOR
        
        private void OnDrawGizmos()
        {
            var origin = Application.isPlaying ? Core.Detection.transform : transform;
            Utils.DrawWireArc2D(origin, _data.checkRadius, _data.checkAngle, Color.green);
        }
        
#endif
    }
}