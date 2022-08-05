using Base;
using UI;
using UnityEngine;

namespace SceneSpecific
{
    public abstract class BasePhaseManager : SingletonMono<BasePhaseManager>
    {
        protected override void Start()
        {
            base.Start();

            UIManager.Instance.ShowPanel<GamePanel>("GamePanel");
        }
    }
}
