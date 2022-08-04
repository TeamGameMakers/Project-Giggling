using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "Data/Monster")]
    public class MonsterDataSo: ScriptableObject
    {
        public MonsterType monsterType;
        public bool isDead;
        public float fadeTime;

        [Header("Attribute")]
        public float walkSpeed;
        public float chaseSpeed;
        public float hitSpeed;
        public int healthPoint;

        [Header("Detection")] 
        public float checkRadius;
        public float checkAngle;
        public LayerMask checkLayer;
        
        [Header("Patrol")]
        public float _patrolStopTime;
        
        public enum MonsterType
        {
            Normal,
            Elite,
            Boss
        };
    }
}