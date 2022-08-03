using System.Collections.Generic;
using Base.FSM;
using Data;
using Core;
using UnityEngine;

namespace Characters.Monsters
{
    public class MonsterState : BaseState
    {
        protected readonly Monster _monster;
        protected readonly GameCore _core;
        protected readonly MonsterDataSo _data;
        private readonly int _animBoolHash;

        protected MonsterState(Monster monster, string name) : base(monster.StateMachine)
        {
            _monster = monster;
            _core = monster.Core;
            _data = monster.Data;
            _animBoolHash = Animator.StringToHash(name);
        }

        public override void Enter()
        {
            _monster.SetAnimBool(_animBoolHash, true);
        }

        public override void PhysicsUpdate()
        {
            if (!_monster.Hit)
                _monster.target = _core.Detection.ArcDetection(_monster.transform, _data.checkRadius,
                    _data.checkAngle, _data.checkLayer);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (_data.healthPoint > 0 && _monster.Hit)
                StateMachine.ChangeState(_monster.ChaseState);
            
            if (_data.monsterType != MonsterDataSo.MonsterType.Boss && _data.healthPoint <= 0)
                StateMachine.ChangeState(_monster.DieState);
        }

        public override void Exit()
        {
            _monster.SetAnimBool(_animBoolHash, false);
        }
    }
}