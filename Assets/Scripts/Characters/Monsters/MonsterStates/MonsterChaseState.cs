using Base.Event;
using Data;
using GM;
using UnityEngine;

namespace Characters.Monsters
{
    public class MonsterChaseState: MonsterState
    {
        private Transform _target;
        private readonly int _animHashVelocityX = Animator.StringToHash("velocityX");
        private readonly int _animHashVelocityY = Animator.StringToHash("velocityY");

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
                    AkSoundEngine.PostEvent("Meet_Boss", _monster.gameObject);
                    break;
                case MonsterDataSO.MonsterType.Boss:
                    Debug.Log("触发");
                    AkSoundEngine.PostEvent("A_humble", _monster.gameObject);
                    AkSoundEngine.PostEvent("Meet_Monster", _monster.gameObject);
                    break;
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            _core.AIMovement.SetSpeed(_monster.Hit? _data.hitSpeed : _data.chaseSpeed);

            if (_monster.target || _monster.HitByPlayer)
            {
                _core.Detection.LookAtTarget(_target);
                if (_monster.target)
                    _target = _monster.target.transform;
            }

            if (_data.monsterType == MonsterDataSO.MonsterType.Boss)
            {
                Debug.Log("Update Anim");
                _monster.SetAnimFloat(_animHashVelocityX, _core.AIMovement.CurrentVelocity.x);
                _monster.SetAnimFloat(_animHashVelocityY, _core.AIMovement.CurrentVelocity.y);
            }
            
            if (!_monster.target && !_monster.HitByPlayer)
            {
                if (_data.monsterType != MonsterDataSO.MonsterType.Boss)
                    StateMachine.ChangeState(_monster.IdleState);
                else
                    StateMachine.ChangeState(_monster.TeleportState);
            }

            if (Vector3.Distance(_monster.transform.position, GM.GameManager.Player.position) <= _data.catchDistance)
            {
                EventCenter.Instance.EventTrigger("GameOver");
            }
        }

        public override void Exit()
        {
            base.Exit();
            AkSoundEngine.PostEvent("StopCombat", _monster.gameObject);
        }
    }
}