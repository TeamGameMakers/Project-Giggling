using UnityEngine;

namespace SceneSpecific
{
    public class Phase2Manager : BasePhaseManager
    {
        protected override void Start()
        {
            base.Start();
            Debug.Log("第二个游戏场景 Start");
        }
    }
}
