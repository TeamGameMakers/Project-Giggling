using UnityEngine;
using Data;
using UnityEngine.Experimental.Rendering.Universal;
using Cinemachine;
using GM;

namespace Interact
{
    public class SecurityCamera: Interactable
    {
        private bool _interacting;
        private Light2D _light;
        private Interactor _interactor;
        private bool _isRotating;
        private CinemachineVirtualCamera _followCamera;

        [SerializeField] private float _size = 5;

        protected override void Awake()
        {
            base.Awake();

            _light = GetComponentInChildren<Light2D>();
            _followCamera = FindObjectOfType<CinemachineVirtualCamera>();
        }

        protected override void Start()
        {
            base.Start();
            _interacting = false;
            _light.enabled = false;
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

            _light.transform.Rotate(eulerAngle);

            if (!_isRotating && InputHandler.NormInputX != 0)
            {
                _isRotating = true;
                AkSoundEngine.PostEvent("CameraLoop", gameObject);
            }
            else if (_isRotating && InputHandler.NormInputX == 0)
            {
                _isRotating = false;
                AkSoundEngine.PostEvent("CaremaStop", gameObject);
            }
        }

        private void ExitCameraControl()
        {
            InputHandler.SwitchToPlayer();
            _interacting = false;
            _light.enabled = false;
            
            _spriteRenderer.gameObject.SetActive(true);
            _interactor.gameObject.SetActive(true);
            _followCamera.m_Follow = GameManager.Player;
            _followCamera.m_Lens.OrthographicSize = 3;
            AkSoundEngine.PostEvent("CameraExit", gameObject);
            AkSoundEngine.PostEvent("CaremaStop", gameObject);
        }

        public override void Interact(Interactor interactor)
        {
            _interactor = interactor;
            InputHandler.SwitchToCamera();
            _interacting = true;
            _light.enabled = true;
            
            _spriteRenderer.gameObject.SetActive(false);
            _interactor.gameObject.SetActive(false);
            _followCamera.m_Follow = transform;
            _followCamera.m_Lens.OrthographicSize = _size;
            AkSoundEngine.PostEvent("CameraStart", gameObject);
        }
    }
}