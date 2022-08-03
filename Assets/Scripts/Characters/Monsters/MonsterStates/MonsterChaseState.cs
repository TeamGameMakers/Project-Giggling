using UnityEngine;

namespace Characters.Monsters.MonsterStates
{
    public class MonsterChaseState: MonsterState
    {
        public MonsterChaseState(Monster monster, string name) : base(monster, name) { }

        public override void Enter()
        {
            base.Enter();
            _core.AIMovement.CurrentDestination = _monster.detected.transform;
            _core.AIMovement.SetSpeed(_data.chaseSpeed);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!_monster.detected) StateMachine.ChangeState(_monster.IdleState);
        }
    }
}