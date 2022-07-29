using UnityEngine;
using Data;

namespace Interact
{
    public class SecurityCamera: Interactable
    {
        private bool _interacting;
        
        protected override void Start()
        {
            base.Start();
            _interacting = false;
        }

        protected override void Update()
        {
            base.Update();
            
            if (!_interacting) return;

            if (InputHandler.ExitPressed) ExitCameraControl();
            
        }

        private void ExitCameraControl()
        {
            InputHandler.SwitchToPlayer();
            _interacting = false;
        }

        public override void Interact(Interactor interactor)
        {
            InputHandler.SwitchToCamera();
            _interacting = true;
        }
    }
}