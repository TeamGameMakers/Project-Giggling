using Save;
using UnityEngine;

namespace SceneSpecific
{
    public class Phase3Building3Manager : BasePhaseManager<Phase3Building3Manager>
    {
        protected override void Start()
        {
            base.Start();
            
            AkSoundEngine.PostEvent("School_indoorFcrazy", gameObject);
            // 检查进入的层数
            string str = SaveManager.GetValue("ReachLayer");
            // 没有存过
            if (string.IsNullOrEmpty(str))
            {
                SaveManager.Register("ReachLayer", "1");
                AkSoundEngine.PostEvent("School_indoorF1", gameObject);
            }
            // 存过了则播放
            else
            {
                int floor = int.Parse(str);
                // 比较层数
                if (floor < 3)
                {
                    SaveManager.Register("ReachLayer", "3");
                    floor = 3;
                }
                Debug.Log($"播放 {floor} 层音效");
                AkSoundEngine.PostEvent("School_indoorF" + floor, gameObject);
            }
        }
    }
}