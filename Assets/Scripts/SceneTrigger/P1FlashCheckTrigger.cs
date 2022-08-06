using Characters.Player;
using Data;
using Data.Story;
using Story;
using UnityEngine;

namespace SceneTrigger
{
    public class P1FlashCheckTrigger : CheckSceneTrigger
    {
        public PlotDataSO plot;

        public Player player;

        protected override void Start()
        {
            base.Start();
            // 检查手电筒状态设置初始状态
            if (CheckTrigger())
                Destroy(gameObject);
        }

        protected override bool CheckTrigger()
        {
            return player.HasFlashLight();
        }

        protected override void PassCheck()
        {
            base.PassCheck();
            // 销毁自身
            Destroy(gameObject);
        }

        protected override void NotPassCheck()
        {
            base.NotPassCheck();
            // 触发对话
            StoryManager.Instance.StartStory(plot);
        }
    }
}
