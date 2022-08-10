using UnityEngine;

namespace SceneSpecific
{
    public class Phase3BuildingManager : BasePhaseManager<Phase3BuildingManager>
    {
        protected override void Start()
        {
            base.Start();
            Debug.Log("进入第三幕场景的 MainBuilding");
        }
    }
}