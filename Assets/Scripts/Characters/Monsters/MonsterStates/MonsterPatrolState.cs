namespace Characters.Monsters
{
    public class MonsterPatrolState: MonsterState
    {
        private readonly int _maxPointNum;
        private int _pointIndex;
        public MonsterPatrolState(Monster monster, string name) : base(monster, name)
        {
            _pointIndex = 0;
            _maxPointNum = _monster.PatrolPoints.Count;
        }

        public override void Enter()
        {
            base.Enter();
            _core.AIMovement.CurrentDestination = _monster.PatrolPoints[_pointIndex];
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            _core.Detection.LookAtTarget(_monster.PatrolPoints[_pointIndex]);
            
            if (_monster.target) 
                StateMachine.ChangeState(_monster.ChaseState);
            else if ((_monster.transform.position - _monster.PatrolPoints[_pointIndex].position).sqrMagnitude < 0.1f)
                StateMachine.ChangeState(_monster.IdleState);
        }

        public override void Exit()
        {
            base.Exit();

            if (++_pointIndex == _maxPointNum) 
                _pointIndex -= _maxPointNum;
        }
    }
}