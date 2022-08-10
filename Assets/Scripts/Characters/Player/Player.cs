using System.Collections;
using System.Collections.Generic;
using Base.Event;
using Base.FSM;
using Characters.Monsters;
using Core;
using Data;
using Save;
using UI;
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
            _monstersColl = new List<Collider2D>();
        }

        private void Start()
        {
            _flashLight.enabled = false;
            GM.GameManager.SetPlayerTransform(transform);

            data = Instantiate(data);
            data.hasFlashLight = SaveManager.GetBool("hasFlashLight");
            
            IdleState = new PlayerIdleState(this, "idle");
            MoveState = new PlayerMoveState(this, "move");
            StateMachine.Initialize(IdleState);

            EventCenter.Instance.AddFuncListener<Collider2D, bool>("LightOnMonster", LightOnMonster);
            
            // 手电筒
            _flashLight.pointLightOuterRadius = data.lightRadius;
            _flashLight.pointLightOuterAngle = data.lightAngle;
            _flashLight.pointLightInnerAngle = data.lightAngle - 10;

            // 恢复存档位置
            string json = SaveManager.GetValue("PlayerPosition");
            if (!string.IsNullOrEmpty(json))
                transform.position = JsonUtility.FromJson<Vector3>(json);
            
            // 注册拾取事件
            RegisterEvent();
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }

        private void Update()
        {
            Core.LogicUpdate();
            StateMachine.CurrentState.LogicUpdate();
            
            if (data.hasFlashLight) Anim.runtimeAnimatorController = playerWithFlashLight;

            FlashLightControl();
            
            // 手电伤害判定
            FlashLightDetect();
            
            // 耐力槽计算
            StaminaCheck();
            
            // 玩家状态UI更新
            UpdateStatusUI();
        }

        private void OnDisable()
        {
            EventCenter.Instance.RemoveFuncListener<Collider2D, bool>("LightOnMonster", LightOnMonster);
            EventCenter.Instance.RemoveEventListener(PickFlashLightEvent, PickFlashLight);
            EventCenter.Instance.RemoveFuncListener(GetPlayerPositionEvent, GetPlayerPosition);
            EventCenter.Instance.RemoveFuncListener(EventGetPlayerFlashLight, GetPlayerFlashLight);

        }

        private void FlashLightControl()
        {
            if (InputHandler.LightPressed && data.hasFlashLight)
            {
                _powerRemaining = EventCenter.Instance.
                    FuncTrigger<float, bool>("UseBatteryPower", data.powerUsingSpeed);
                
                InputHandler.UseLightInput();

                if (_powerRemaining && !_flashLight.enabled)
                {
                    _flashLight.enabled = true;
                    AkSoundEngine.PostEvent("Flashlight_on", gameObject);
                }
                else
                {
                    _flashLight.enabled = false;
                    _monstersColl = new List<Collider2D>();
                    AkSoundEngine.PostEvent("Flashlight_off", gameObject);
                }
            }
            else if (_flashLight.enabled && !_powerRemaining)
            {
                _flashLight.enabled = false;
                AkSoundEngine.PostEvent("Battery_ranout", gameObject);
            }

            if (InputHandler.ReloadPressed)
            {
                EventCenter.Instance.EventTrigger("UseBattery");
                InputHandler.UseReloadInput();
            }
            
            if (_flashLight.enabled)
                _powerRemaining = EventCenter.Instance.
                    FuncTrigger<float, bool>("UseBatteryPower", data.powerUsingSpeed);
        }

        private void FlashLightDetect()
        {
            if (!_flashLight.enabled) return;
            
            _monstersColl = Core.Detection.ArcDetectionAll(Core.Detection.transform, 
                data.lightRadius, data.lightAngle * 0.5f, data.layer, "Monster");

            // 伤害判定
            foreach (var coll in _monstersColl) 
                Monster.Monsters[coll.GetInstanceID()].MonsterStayLight(data.lightDamage);
        }

        private void StaminaCheck()
        {
            if (InputHandler.SprintPressed && StateMachine.CurrentState != IdleState)
                data.stamina -= data.staminaSpeed * Time.deltaTime;
            else
                data.stamina += data.staminaSpeed * Time.deltaTime;

            data.stamina = Mathf.Clamp(data.stamina, 0, data.maxStamina);
            data.canSprint = data.stamina > 0;
            
            EventCenter.Instance.EventTrigger("UpdateStamina", data.stamina / data.maxStamina);
        }

        private bool LightOnMonster(Collider2D coll) => _monstersColl.Contains(coll);

        /// <summary>
        /// 进入光
        /// </summary>
        public void PlayerStayLight(float damage)
        {
            StopCoroutine(RestoreHp());
            data.healthPoint -= damage * Time.deltaTime;
            data.healthPoint = Mathf.Clamp(data.healthPoint, 0f, data.maxHealthPoint);

            if (data.healthPoint == 0)
            {
                UIManager.Instance.ShowGameOverPanel();
            }
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

        private void UpdateStatusUI()
        {
            EventCenter.Instance.EventTrigger("UpdateHealth", data.healthPoint / data.maxHealthPoint);
            EventCenter.Instance.EventTrigger("UpdateHealth", data.healthPoint / data.maxHealthPoint);
        }
        

        #region 手电筒拾取

        public const string PickFlashLightEvent = "PickFlashLight";
        
        private void PickFlashLight()
        {
            Debug.Log("玩家获得手电筒");
            data.hasFlashLight = true;
        }

        public bool HasFlashLight()
        {
            return data.hasFlashLight;
        }

        #endregion

        #region 获取玩家数据

        public const string GetPlayerPositionEvent = "GetPlayerPosition";

        private string GetPlayerPosition() => JsonUtility.ToJson(transform.position);

        public const string EventGetPlayerFlashLight = "GetPlayerFlashLight";

        private bool GetPlayerFlashLight() => data.hasFlashLight;

        #endregion
        
        private void RegisterEvent()
        {
            EventCenter.Instance.AddEventListener(PickFlashLightEvent, PickFlashLight);
            EventCenter.Instance.AddFuncListener<string>(GetPlayerPositionEvent, GetPlayerPosition);
            EventCenter.Instance.AddFuncListener<bool>(EventGetPlayerFlashLight, GetPlayerFlashLight);
        }
    }
}
