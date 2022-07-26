using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputHandler : MonoBehaviour
{
    private PlayerInput _playerInput;

    private InputActionMap _playerMap;
    private InputActionMap _cameraMap;
    private InputActionMap _lockPickMap;

    private InputAction _move;
    
    private InputAction _rotate;
    
    // Player
    public Vector2 RawMoveInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    
    // Camera
    public bool InteractInput { get; private set; }
    public bool ExitInput { get; private set; }
    
    // Lock Pick
    public bool PryInput { get; private set; }
    public bool PickInput { get; private set; }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerMap = _playerInput.actions.FindActionMap("Player");
        _cameraMap = _playerInput.actions.FindActionMap("SurveillanceCam");
        _lockPickMap = _playerInput.actions.FindActionMap("Lock Pick");
    }

    private void Start()
    {
        // Player
        _playerMap.actionTriggered += OnPlayerMoveInput;
        _playerMap.actionTriggered += OnCamRotateInput;
        
        // Camera
        _cameraMap.actionTriggered += OnInteractInput;
        _cameraMap.actionTriggered += OnExitInput;

        // Lock Pick
        _lockPickMap.actionTriggered += OnPryInput;
        _lockPickMap.actionTriggered += OnPickInput;
    }

    #region Actions
    
    // Player
    private void OnPlayerMoveInput(InputAction.CallbackContext context)
    {
        if (context.action != _move) return;
        RawMoveInput = context.ReadValue<Vector2>();
        NormInputX = Mathf.RoundToInt(RawMoveInput.x);
        NormInputY = Mathf.RoundToInt(RawMoveInput.y);
    }
    
    private void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.action.name != "Interact") return;

        InteractInput = context.performed;
    }
    
    // Surveillance Camera
    private void OnCamRotateInput(InputAction.CallbackContext context)
    {
        if (context.action.name != "Rotate") return;
        NormInputX = Mathf.RoundToInt(context.ReadValue<float>());
        Debug.Log(NormInputX);
    }

    private void OnExitInput(InputAction.CallbackContext context)
    {
        if (context.action.name != "Exit") return;

        ExitInput = context.performed;
    }

    private void OnPryInput(InputAction.CallbackContext context)
    {
        if (context.action.name != "Pry") return;

        PryInput = context.performed;
    }

    private void OnPickInput(InputAction.CallbackContext context)
    {
        if (context.action.name != "Pick") return;

        PickInput = context.performed;
    }
    
    #endregion

    public void SwitchToPlayer() => _playerInput.SwitchCurrentActionMap("Player");
    public void SwitchToCamera() => _playerInput.SwitchCurrentActionMap("SurveillanceCam");
}
