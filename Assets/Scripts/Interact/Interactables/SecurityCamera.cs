using UnityEngine;
using Data;

namespace Interact
{
    public class SecurityCamera: MonoBehaviour, IInteractable
    {
        private bool _interacting;
        [SerializeField] private SecurityCameraSO _data;
        [SerializeField] private string _interactPrompt;

        public string InteractPrompt => _interactPrompt;

        private void Start()
        {
            _interacting = false;
        }

        private void Update()
        {
            if (!_interacting) return;

            if (InputHandler.ExitPressed) Exit();
            
            
        }

        private void Exit()
        {
            InputHandler.SwitchToPlayer();
            _interacting = false;
        }

        public void Interact(Interactor interactor)
        {
            InputHandler.SwitchToCamera();
            _interacting = true;
        }
    }
}