using Save;
using UnityEngine;

namespace SceneSpecific
{
    public class Phase3Manager : BasePhaseManager<Phase3Manager>
    {
        protected override void Start()
        {
            base.Start();
            Debug.Log("进入第三幕场景");
            SaveManager.Save();
        }
    }
}