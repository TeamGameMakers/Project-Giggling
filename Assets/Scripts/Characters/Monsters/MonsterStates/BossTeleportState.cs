using System.Collections;
using UnityEngine;

namespace Characters.Monsters
{
    public class BossTeleportState: MonsterState
    {
        private bool _canTeleport;
        private bool _countDowning;
        
        public BossTeleportState(Monster monster, string name = null) : base(monster, name) { }

        public override void Enter()
        {
            base.Enter();

            _canTeleport = false;
            _countDowning = false;
            
            _monster.transform.position = TransformRandom.Instance.GetRandomPosition();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (_data.healthPoint > 0) _data.healthPoint -= Time.deltaTime;
            
            if (!_countDowning && !_canTeleport)
                _monster.StartCoroutine(TeleportTimer(_data._patrolStopTime));

            if (_monster.target)
                StateMachine.ChangeState(_monster.ChaseState);

            else if (_canTeleport)
            {
                _monster.transform.position = TransformRandom.Instance.GetRandomPosition();
                _canTeleport = false;
            }
        }

        public override void Exit()
        {
            base.Exit();
            
            _monster.StopCoroutine(TeleportTimer(_data._patrolStopTime));
        }

        private IEnumerator TeleportTimer(float timer)
        {
            _countDowning = true;
            
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }

            _canTeleport = true;
            _countDowning = false;
        }
    }
}