using System;
using Save;
using UnityEngine;

namespace Story
{
    public class LastBossRegister : MonoBehaviour
    {
        public string registerKey;

        protected virtual void OnDisable()
        {
            SaveManager.RegisterBool(registerKey);
        }
    }
}