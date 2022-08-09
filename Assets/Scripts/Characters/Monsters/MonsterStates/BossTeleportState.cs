using System.Collections;
using Function;
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
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!_countDowning && !_canTeleport)
                _monster.StartCoroutine(TeleportTimer(_data._patrolStopTime));

            if (_canTeleport)
            {
                RandomSelector.
            }
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