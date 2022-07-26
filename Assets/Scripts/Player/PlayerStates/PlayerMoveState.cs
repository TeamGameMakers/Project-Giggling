using UnityEngine;

namespace Player
{
    public class PlayerMoveState: PlayerState
    {
        public PlayerMoveState(Player player, string name) : base(player, name) { }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            core.Movement.SetVelocity(InputVec2 * data.moveVelocity);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (InputVec2 == Vector2.zero)
                StateMachine.ChangeState(player.IdleState);
        }
    }
}