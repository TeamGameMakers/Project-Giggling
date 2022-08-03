using UnityEngine;

namespace Characters.Monsters
{
    public class MonsterChaseState: MonsterState
    {
        public MonsterChaseState(Monster monster, string name) : base(monster, name) { }

        public override void Enter()
        {
            base.Enter();
            _core.AIMovement.CurrentDestination = _monster.target.transform;
            _core.AIMovement.SetSpeed(_data.chaseSpeed);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Debug.Log(_monster.target);
            if (!_monster.target) StateMachine.ChangeState(_monster.IdleState);
            else _core.Detection.LookAtTarget(_monster.target.transform);
        }
    }
}