using Base.Event;
using Base.FSM;
using Core;
using Data;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Characters.Player
{
    public class Player : Entity
    {
        internal PlayerStateMachine StateMachine { get; private set; }
        internal Animator Anim { get; private set; }
        internal GameCore Core { get; private set; }

        private Light2D _flashLight;
        private bool _powerRemaining;
        
        public PlayerDataSO data;

        #region States
    
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
    
        #endregion

        private void Awake()
        {
            Anim = GetComponent<Animator>();
            Core = GetComponentInChildren<GameCore>();
            _flashLight = GetComponentInChildren<Light2D>();

            StateMachine = new PlayerStateMachine();
            IdleState = new PlayerIdleState(this, "idle");
            MoveState = new PlayerMoveState(this, "move");
        }

        private void Start()
        {
            _flashLight.enabled = false;
            StateMachine.Initialize(IdleState);
            GM.GameManager.SetPlayerTransform(transform);
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }

        private void Update()
        {
            Core.LogicUpdate();
            StateMachine.CurrentState.LogicUpdate();

            FlashLightControl();
        }

        private void FlashLightControl()
        {
            if (InputHandler.LightPressed && data.hasFlashLight)
            {
                _powerRemaining = EventCenter.Instance.
                    EventTrigger<float, bool>("UseBatteryPower", data.powerUsingSpeed);
                
                InputHandler.UseLightInput();
                
                if (_powerRemaining && !_flashLight.enabled)
                    _flashLight.enabled = true;
                
                else
                    _flashLight.enabled = false;
            }
            
            else if (_flashLight.enabled && !_powerRemaining)
                _flashLight.enabled = false;
            
            if (_flashLight.enabled)
                _powerRemaining = EventCenter.Instance.
                    EventTrigger<float, bool>("UseBatteryPower", data.powerUsingSpeed);
            
            if (InputHandler.RawMoveInput != Vector2.zero)
                _flashLight.transform.up = InputHandler.RawMoveInput;
        }
        
        /// <summary>
        /// 进入光
        /// </summary>
        public void PlayerEnterLight(int damage)
        {
            Debug.Log("玩家进入光");
        }

        /// <summary>
        /// 离开光
        /// </summary>
        public void PlayerExitLight()
        {
            Debug.Log("玩家离开光");
        }
    }
}
