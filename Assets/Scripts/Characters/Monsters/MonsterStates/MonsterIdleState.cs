using UnityEngine;

namespace Characters.Monsters.MonsterStates
{
    public class MonsterIdleState: MonsterState
    {
        public MonsterIdleState(Monster monster, string name) : base(monster, name) { }

        public override void Enter()
        {
            base.Enter();
            _core.AIMovement.CurrentDestination = _monster.transform;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (_detected) StateMachine.ChangeState(_monster.ChaseState);
        }
    }
}