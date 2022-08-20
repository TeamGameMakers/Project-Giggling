using Save;
using UnityEngine;

namespace SceneSpecific
{
    public class Phase3Building2Manager : BasePhaseManager<Phase3Building2Manager>
    {
        protected override void Start()
        {
            base.Start();
            Debug.Log("进入第三幕第二层");
            
            if (SaveManager.GetBool("MeetGirl"))
            {
                AkSoundEngine.PostEvent("School_indoorFcrazy", gameObject);
            }
            else
            {
                // 检查进入的层数
                string str = SaveManager.GetValue("ReachLayer");
                int floor = int.Parse(str);

                // 比较层数
                if (floor < 2)
                {
                    SaveManager.Register("ReachLayer", 2);
                    floor = 2;
                }

                Debug.Log($"播放 {floor} 层音效");
                AkSoundEngine.PostEvent("School_indoorF" + floor, gameObject);

                if (SaveManager.GetBool("MeetGirl"))
                    AkSoundEngine.PostEvent("School_indoorFcrazy", gameObject);
            }
        }
    }
}