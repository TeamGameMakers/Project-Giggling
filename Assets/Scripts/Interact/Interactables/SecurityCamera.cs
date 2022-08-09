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

            CameraControl();

            if (InputHandler.ExitPressed) ExitCameraControl();
            
        }

        private void CameraControl()
        {
            var eulerAngle = new Vector3(0.0f, 0.0f,
                -InputHandler.NormInputX * ((SecurityCameraDataSO)_data).rotationSpeed * Time.deltaTime);
            
            var maxAngle = ((SecurityCameraDataSO)_data).maxAngle;

            if (transform.eulerAngles.z >= maxAngle && transform.eulerAngles.z >= 0 && InputHandler.NormInputX < 0)
                eulerAngle = Vector3.zero;
            else if (360 - transform.eulerAngles.z >= maxAngle && transform.eulerAngles.z > 0 && InputHandler.NormInputX > 0)
                eulerAngle = Vector3.zero;

            transform.Rotate(eulerAngle);
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