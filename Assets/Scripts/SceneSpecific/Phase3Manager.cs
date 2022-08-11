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

            // 没见到女主才调用
            if (SaveManager.GetBool("MeetGirl"))
            {
                AkSoundEngine.PostEvent("School_indoorFcrazy", gameObject);
                AkSoundEngine.PostEvent("School_outdoor", gameObject);
            }
            else
                AkSoundEngine.PostEvent("Neiberhood", gameObject);
        }
    }
}