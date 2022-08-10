using System.Collections.Generic;
using Base.Event;
using Base.FSM;
using Core;
using Data;
using GM;
using Save;
using UnityEngine;
using Utilities;

namespace Characters.Monsters
{
    public class Monster: Entity
    {
        [SerializeField] private MonsterDataSO _data;
        private Animator _anim;
        private Collider2D _coll;

        public static readonly Dictionary<int, Monster> Monsters;
        
        internal MonsterStateMachine StateMachine { get; private set; }
        internal GameCore Core { get; private set; }
        internal MonsterDataSO Data => _data;
        internal Collider2D target;
        internal Transform SpawnTransform { get; private set; }
        
        public MonsterIdleState IdleState { get; private set; }
        public MonsterChaseState ChaseState { get; private set; }
        public MonsterPatrolState PatrolState { get; private set; }
        public MonsterDieState DieState { get; private set; }

        public BossTeleportState TeleportState { get; private set; }
        
        public List<Transform> PatrolPoints { get; private set; }
        public bool Patrol { get; private set; }
        public bool Hit { get; private set; }
        public bool HitByPlayer { get; private set; }

        static Monster()
        {
            Monsters = new Dictionary<int, Monster>();
        }
        
        private void Awake()
        {
            Core = GetComponentInChildren<GameCore>();
            _anim = GetComponent<Animator>();
            _coll = GetComponent<Collider2D>();
            Monsters.Add(_coll.GetInstanceID(), this);
            
            StateMachine = new MonsterStateMachine();
            
            _data = Instantiate(_data);
            _data.isDead =  SaveManager.GetBool(this.GetInstanceID().ToString());
            
            if (_data.monsterType == MonsterDataSO.MonsterType.Boss)
            {
                TeleportState = new BossTeleportState(this);
                ChaseState = new MonsterChaseState(this, "move");
            }
            else
            {
                IdleState = new MonsterIdleState(this);
                ChaseState = new MonsterChaseState(this);
                PatrolState = new MonsterPatrolState(this);
            }
            DieState = new MonsterDieState(this);
        }

        private void Start()
        {
            Patrol = CheckPatrol();
            
            SpawnTransform = new GameObject("Spawn Point").transform;
            SpawnTransform.parent = transform.parent;
            SpawnTransform.position = transform.position;
            SpawnTransform.right = Core.Detection.transform.right;

            if (_data.isDead)
                MonsterDie();

            if (_data.monsterType != MonsterDataSO.MonsterType.Boss)
                StateMachine.Initialize(IdleState);
            else
                StateMachine.Initialize(TeleportState);
        }

        private void FixedUpdate()
        {
            if (GameManager.State == GameState.UI) return;
            
            StateMachine.CurrentState.PhysicsUpdate();
        }

        private void Update()
        {
            if (GameManager.State == GameState.UI) return;

            Core.LogicUpdate();
            StateMachine.CurrentState.LogicUpdate();
            MonsterExitFlashLight();
        }

        private void OnDestroy()
        {
            Monsters.Remove(_coll.GetInstanceID());
        }

        internal void SetAnimBool(int hash, bool value)
        {
            _anim.SetBool(hash, value);
        }

        internal void SetAnimFloat(int hash, float value)
        {
            _anim.SetFloat(hash, value);
        }

        private bool CheckPatrol()
        {
            if (transform.parent.childCount < 2) return false;
            var points = transform.parent.GetChild(1);
            if (points.childCount < 2) return false;
            
            PatrolPoints = new List<Transform>();
            for (int i = 0; i < points.childCount; i++)
                PatrolPoints.Add(points.GetChild(i));
            
            return true;
        }
        
        /// <summary>
        /// 怪进入光
        /// </summary>
        public void MonsterStayLight(float damage)
        {
            if (!HitByPlayer) AkSoundEngine.PostEvent("Monster_burn", gameObject);

            Hit = true;
            _data.healthPoint -= damage * Time.deltaTime;
            HitByPlayer = true;
        }

        public void MonsterStayRoadLight(float damage)
        {
            Hit = true;
            HitByPlayer = false;
            _data.healthPoint -= damage * Time.deltaTime;
        }
        

        /// <summary>
        /// 怪离开光
        /// </summary>
        private void MonsterExitFlashLight()
        {
            if (EventCenter.Instance.FuncTrigger<Collider2D, bool>("LightOnMonster", _coll)) return;
            
            if (HitByPlayer)
                AkSoundEngine.PostEvent("MonsterStopBurn", gameObject);
            
            Hit = false;
            HitByPlayer = false;
            
        }
        
        /// <summary>
        /// 怪离开路灯
        /// </summary>
        public void MonsterExitRoadLight()
        {
            Hit = false;
            HitByPlayer = false;
        }

        public void MonsterDie()
        {
            var curTransform = transform;
            
            if (_data.monsterType != MonsterDataSO.MonsterType.Boss)
                SaveManager.RegisterBool(this.GetInstanceID().ToString());
            
            var parent = curTransform.parent;
            curTransform.parent = null;
            
            Destroy(parent.gameObject);
            if (!Patrol)
                Destroy(SpawnTransform.gameObject);
            Destroy(curTransform.gameObject);
        }

#if UNITY_EDITOR
        
        private void OnDrawGizmos()
        {
            var origin = Application.isPlaying ? Core.Detection.transform : transform;
            Utils.DrawWireArc2D(origin, _data.checkRadius, _data.checkAngle, Color.green);
        }
        
#endif
    }
}