using Base.FSM;
using Characters.Monsters.MonsterStates;
using Core;
using Data;
using UnityEngine;

namespace Characters.Monsters
{
    public class Monster: Entity
    {
        [SerializeField] private MonsterDataSo _data;
        private Animator _anim;
        
        internal MonsterStateMachine StateMachine { get; private set; }
        internal GameCore Core { get; private set; }
        internal MonsterDataSo Data => _data;
        internal Collider2D detected;
        
        public MonsterIdleState IdleState { get; private set; }
        public MonsterChaseState ChaseState { get; private set; }

        private void Awake()
        {
            Core = GetComponentInChildren<GameCore>();
            _anim = GetComponent<Animator>();
            
            StateMachine = new MonsterStateMachine();
            IdleState = new MonsterIdleState(this, "idle");
            ChaseState = new MonsterChaseState(this, "chase");
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
    }
}