using UnityEngine;

namespace Interact
{
    internal class Interaction
    {
        private InputHandler _input;
        
        protected Interaction(InputHandler input)
        {
            _input = input;
        }

        public virtual void Enter() {}
        public virtual void PhysicsUpdate() {}
        public virtual void LogicUpdate() {}
        public virtual void Exit() {}
    }
}