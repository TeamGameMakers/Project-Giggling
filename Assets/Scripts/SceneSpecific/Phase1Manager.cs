using UnityEngine;

namespace SceneSpecific
{
    public class Phase1Manager : BasePhaseManager
    {
        protected override void Start()
        {
            base.Start();
            Debug.Log("第一个游戏场景 Start");
        }
    }
}
