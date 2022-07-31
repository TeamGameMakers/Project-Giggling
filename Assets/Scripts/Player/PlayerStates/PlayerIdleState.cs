using UnityEngine;

namespace Player
{
    public sealed class PlayerIdleState: PlayerState
    {
        public PlayerIdleState(global::Player.Player player, string name) : base(player, name) { }

        public override void Enter()
        {
            base.Enter();
            _core.Movement.SetVelocity(Vector2.zero);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (InputVec2 != Vector2.zero) 
                StateMachine.ChangeState(_player.MoveState);
        }
    }
}