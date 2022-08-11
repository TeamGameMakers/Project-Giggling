using System;
using Save;
using UnityEngine;

namespace Story
{
    public class LastBossRegister : MonoBehaviour
    {
        public string registerKey;

        private void OnEnable()
        {
            
        }

        protected virtual void OnDisable()
        {
            SaveManager.RegisterBool(registerKey);
        }
    }
}