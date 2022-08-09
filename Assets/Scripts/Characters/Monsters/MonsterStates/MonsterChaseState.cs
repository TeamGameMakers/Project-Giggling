using Data;
using UnityEngine;

namespace Characters.Monsters
{
    public class MonsterChaseState: MonsterState
    {
        private Transform _target;
        
        public MonsterChaseState(Monster monster, string name = null) : base(monster, name) { }

        public override void Enter()
        {
            base.Enter();

            _target = _monster.HitByPlayer ? GM.GameManager.Player : _monster.target.transform;
            _core.AIMovement.CurrentDestination = _target;
            
            switch (_data.monsterType)
            {
                case MonsterDataSO.MonsterType.Elite:
                    AkSoundEngine.PostEvent("B_humble", _monster.gameObject);
                    break;
                case MonsterDataSO.MonsterType.Boss:
                    AkSoundEngine.PostEvent("A_humble", _monster.gameObject);
                    break;
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (_monster.target || _monster.HitByPlayer)
                _core.Detection.LookAtTarget(_target);
            
            if (_data.monsterType != MonsterDataSO.MonsterType.Boss)
                _core.AIMovement.SetSpeed(_monster.Hit? _data.hitSpeed : _data.chaseSpeed);

            if (!_monster.target && !_monster.HitByPlayer) StateMachine.ChangeState(_monster.IdleState);
            
        }

        public override void Exit()
        {
            base.Exit();
            AkSoundEngine.PostEvent("StopCombat", _monster.gameObject);
        }
    }
}