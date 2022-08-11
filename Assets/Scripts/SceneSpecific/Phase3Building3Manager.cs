using Save;
using UnityEngine;

namespace SceneSpecific
{
    public class Phase3Building3Manager : BasePhaseManager<Phase3Building3Manager>
    {
        protected override void Start()
        {
            base.Start();
            Debug.Log("进入第三幕第三层");
            
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
                if (floor < 3)
                {
                    SaveManager.Register("ReachLayer", 3);
                    floor = 3;
                }

                Debug.Log($"播放 {floor} 层音效");
                AkSoundEngine.PostEvent("School_indoorF" + floor, gameObject);

                if (SaveManager.GetBool("MeetGirl"))
                    AkSoundEngine.PostEvent("School_indoorFcrazy", gameObject);
            }
        }
    }
}