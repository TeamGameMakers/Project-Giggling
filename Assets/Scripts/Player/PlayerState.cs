using Base.FSM;
using Core;
using Data;
using UnityEngine;

namespace Player
{
    public class PlayerState : BaseState
    {
        private int _animBoolHash;
    
        protected readonly Player player;
        protected readonly Animator anim;
        protected readonly InputHandler input;
        protected readonly GameCore core;
        protected readonly PlayerDataSO data;

        protected Vector2 InputVec2 { get; private set; }

        public PlayerState(Player player, string name) : base(player.StateMachine)
        {
            this.player = player;
            _animBoolHash = Animator.StringToHash(name);

            anim = player.Anim;
            input = player.InputHandler;
            core = player.Core;
            data = player.data;
        }


        public override void Enter()
        {
            anim.SetBool(_animBoolHash, true);
        }

        public override void LogicUpdate()
        {
            InputVec2 = InputHandler.RawMoveInput;
        }

        public override void Exit()
        {
            anim.SetBool(_animBoolHash, false);
        }
    }
}
