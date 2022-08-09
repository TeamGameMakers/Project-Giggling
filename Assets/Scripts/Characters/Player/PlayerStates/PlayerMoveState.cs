using UnityEngine;

namespace Characters.Player
{
    public class PlayerMoveState: PlayerState
    {
        private readonly int _animHashFloatX = Animator.StringToHash("velocityX");
        private readonly int _animHashFloatY = Animator.StringToHash("velocityY");
        
        public PlayerMoveState(Characters.Player.Player player, string name) : base(player, name) { }

        public override void Enter()
        {
            base.Enter();

            MoveDirection();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            MoveDirection();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (InputVec2 == Vector2.zero)
                StateMachine.ChangeState(_player.IdleState);
            else
            {
                _anim.SetFloat(_animHashFloatX, _core.Movement.CurrentVelocityNorm.x);
                _anim.SetFloat(_animHashFloatY, _core.Movement.CurrentVelocityNorm.y);
            }
        }
        
        private void MoveDirection()
        {
            var curSpeed = InputHandler.SprintPressed ? _data.runVelocity : _data.walkVelocity;
            
            if (InputHandler.NormInputX != 0)
            {
                _core.Movement.SetVelocityX(InputHandler.NormInputX * curSpeed);
                _core.Movement.SetVelocityY(0);
            }
            else if (InputHandler.NormInputY != 0)
            {
                _core.Movement.SetVelocityY(InputHandler.NormInputY * curSpeed);
                _core.Movement.SetVelocityX(0);
            }
        }
    }
}