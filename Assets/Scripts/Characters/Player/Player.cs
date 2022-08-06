using System.Collections;
using System.Collections.Generic;
using Base.Event;
using Base.FSM;
using Characters.Monsters;
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
        private List<Collider2D> _monstersColl;

        public PlayerDataSO data;
        public RuntimeAnimatorController playerWithFlashLight;

        #region States
    
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
    
        #endregion

        private void Awake()
        {
            Anim = GetComponent<Animator>();
            Core = GetComponentInChildren<GameCore>();
            _flashLight = transform.GetChild(2).GetComponent<Light2D>();

            StateMachine = new PlayerStateMachine();
            IdleState = new PlayerIdleState(this, "idle");
            MoveState = new PlayerMoveState(this, "move");
            _monstersColl = new List<Collider2D>();
        }

        private void Start()
        {
            _flashLight.enabled = false;
            StateMachine.Initialize(IdleState);
            GM.GameManager.SetPlayerTransform(transform);
            EventCenter.Instance.AddEventListener<Collider2D, bool>("LightOnMonster", LightOnMonster);
            
            // 手电筒
            if (data.hasFlashLight) Anim.runtimeAnimatorController = playerWithFlashLight;
            _flashLight.pointLightOuterRadius = data.lightRadius;
            _flashLight.pointLightOuterAngle = data.lightAngle;
            _flashLight.pointLightInnerAngle = data.lightAngle - 10;
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
            
            // 手电伤害判定
            if (_flashLight.enabled)
            {
                _monstersColl = Core.Detection.ArcDetectionAll(_flashLight.transform, 
                    data.lightRadius, data.lightAngle, data.layer);
                
                foreach (var coll in _monstersColl)
                    Monster.Monsters[coll.GetInstanceID()].MonsterEnterLight(data.lightDamage, true);
            }
        }

        private void OnDisable()
        {
            EventCenter.Instance.RemoveEventListener<Collider2D, bool>("LightOnMonster", LightOnMonster);
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
            {
                _flashLight.transform.up = InputHandler.RawMoveInput.normalized;
                Core.Detection.transform.right = InputHandler.RawMoveInput.normalized;
            }
        }

        private bool LightOnMonster(Collider2D coll) => _monstersColl.Contains(coll);

        /// <summary>
        /// 进入光
        /// </summary>
        public void PlayerEnterLight(float damage)
        {
            StopCoroutine(RestoreHp());
            data.healthPoint -= damage * Time.deltaTime;
        }

        /// <summary>
        /// 离开光
        /// </summary>
        public void PlayerExitLight() => StartCoroutine(RestoreHp());
        

        private IEnumerator RestoreHp()
        {
            while (data.healthPoint < data.maxHealthPoint)
            {
                data.healthPoint += data.hpRestoreSpeed * Time.deltaTime;
                yield return null;
            }
        }
    }
}
