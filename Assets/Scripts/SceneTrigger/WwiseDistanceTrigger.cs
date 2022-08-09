using System;
using System.Transactions;
using UnityEngine;

namespace SceneTrigger
{
    public class WwiseDistanceTrigger : WwiseTrigger
    {
        public string wwiseRtpcKey;
        
        public Transform targetPosition;
        public Transform listenerPosition;
        public float maxDistance = 50;

        // 开始计算距离的位置
        protected Vector3 originalPosition;

        protected bool inRtpc = false;
        
        protected virtual void Update()
        {
            if (inRtpc)
            {
                //AkSoundEngine.SetRTPCValue(wwiseRtpcKey,);
            }
        }

        protected override void OnTriggerEnter2D(Collider2D col)
        {
            base.OnTriggerEnter2D(col);
            // 记录位置
            inRtpc = true;
        }
    }
}