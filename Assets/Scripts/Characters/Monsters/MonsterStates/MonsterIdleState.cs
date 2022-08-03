using System.Collections;
using UnityEngine;

namespace Characters.Monsters
{
    public class MonsterIdleState: MonsterState
    {
        private bool _canPatrol;
        
        public MonsterIdleState(Monster monster, string name) : base(monster, name) { }

        public override void Enter()
        {
            base.Enter();
            _core.AIMovement.CurrentDestination = _monster.transform;
            
            if (_data.patrol)
            {
                _canPatrol = false;
                _monster.StartCoroutine(StopTimer(_data._patrolStopTime));
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (_monster.target) 
                StateMachine.ChangeState(_monster.ChaseState);
            else if (_canPatrol)
                StateMachine.ChangeState(_monster.PatrolState);
        }

        public override void Exit()
        {
            base.Exit();
            
            if (_monster.target && _data.patrol)
                _monster.StopCoroutine(StopTimer(_data._patrolStopTime));
        }

        private IEnumerator StopTimer(float timer)
        {
            while (timer > 0.0f)
            {
                timer -= Time.deltaTime;
                yield return null;
            }

            _canPatrol = true;
        }
    }
}