using Save;
using UnityEngine.UI;

namespace UI
{
    public class GamePanel : BasePanel
    {
        protected Button settingBtn;
        protected Button saveBtn;
        protected Button backpackBtn;
        
        protected override void Start()
        {
            base.Start();
            // 绑定按钮
            settingBtn = GetControl<Button>("SettingBtn");
            saveBtn = GetControl<Button>("SaveBtn");
            backpackBtn = GetControl<Button>("BackpackBtn");
            settingBtn.onClick.AddListener(() => UIManager.Instance.ShowPanel<SettingPanel>("SettingPanel"));
            saveBtn.onClick.AddListener(() => SaveManager.Save());
            // TODO: 出现背包界面
        }
    }
}
