using UnityEngine;
using UnityEngine.InputSystem;
using Base;

[RequireComponent(typeof(PlayerInput))]
public class InputHandler : SingletonMono<InputHandler>
{
    private PlayerInput _playerInput;

    private InputActionMap _playerMap;
    private InputActionMap _cameraMap;
    private InputActionMap _lockPickMap;

    // Player
    public static Vector2 RawMoveInput { get; private set; }
    public static int NormInputX { get; private set; }
    public static int NormInputY { get; private set; }
    
    // Camera
    public static bool InteractInput { get; private set; }
    public static bool ExitInput { get; private set; }
    
    // Lock Pick
    public static bool PryInput { get; private set; }
    public static bool PickInput { get; private set; }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        
        _playerMap = _playerInput.actions.FindActionMap("Player", true);
        _cameraMap = _playerInput.actions.FindActionMap("SurveillanceCam", true);
        _lockPickMap = _playerInput.actions.FindActionMap("Lock Pick", true);
    }

    private void Start()
    {
        // Player
        _playerMap.actionTriggered += OnPlayerMoveInput;
        _playerMap.actionTriggered += OnInteractInput;
        
        // Camera
        _cameraMap.actionTriggered += OnCamRotateInput;
        _cameraMap.actionTriggered += OnExitInput;

        // Lock Pick
        _lockPickMap.actionTriggered += OnPryInput;
        _lockPickMap.actionTriggered += OnPickInput;
    }

    #region Actions
    
    // Player
    private void OnPlayerMoveInput(InputAction.CallbackContext context)
    {
        if (context.action.name != "Move") return;
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

    // Lock Pick
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
    public void SwitchToLockPick() => _playerInput.SwitchCurrentActionMap("Lock Pick");
}
