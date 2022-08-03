using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "Data/Monster")]
    public class MonsterDataSo: ScriptableObject
    {
        public MonsterType monsterType;

        [Header("Attribute")]
        public float walkSpeed;
        public float chaseSpeed;
        public int healthPoint;

        [Header("Detection")] 
        public float checkRadius;
        public float checkAngle;
        public LayerMask checkLayer;
        
        [Header("Patrol")]
        public bool patrol;
        public float _patrolStopTime;
        
        public enum MonsterType
        {
            Normal,
            Elite,
            Boss
        };
    }
}