using Save;
using UnityEngine;

namespace SceneSpecific
{
    public class Phase2Manager : BasePhaseManager<Phase2Manager>
    {
        protected override void Start()
        {
            base.Start();
            Debug.Log("第二个游戏场景 Start");
            
            // 进入场景就进行一次存档
            SaveManager.Save();
        }
    }
}
