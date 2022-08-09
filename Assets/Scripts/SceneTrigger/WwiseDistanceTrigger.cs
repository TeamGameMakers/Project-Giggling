using System;
using System.Transactions;
using UnityEngine;

namespace SceneTrigger
{
    public class WwiseDistanceTrigger : WwiseTrigger
    {
        public string wwiseRtpcKey;
        
        public Transform targetPosition;
        protected Vector3 target;
        public Transform listenerPosition;

        protected bool inRtpc = false;
        
        public float stopTime;
        protected float curTime = 0;

        protected override void Start()
        {
            base.Start();
            target = targetPosition.position;
        }

        protected virtual void Update()
        {
            if (inRtpc)
            {
                curTime = Time.deltaTime;
                // 计算距离
                float dis = (target - listenerPosition.position).magnitude;
                AkSoundEngine.SetRTPCValue(wwiseRtpcKey, dis);

                if (curTime >= stopTime)
                {
                    inRtpc = false;
                    curTime = 0;
                }
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