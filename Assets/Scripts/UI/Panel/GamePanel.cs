using Save;
using UnityEngine.UI;

namespace UI
{
    public class GamePanel : BasePanel
    {
        protected Button settingBtn;
        protected Button saveBtn;
        
        protected override void Start()
        {
            base.Start();
            // 绑定按钮
            settingBtn = GetControl<Button>("SettingBtn");
            saveBtn = GetControl<Button>("SaveBtn");
            settingBtn.onClick.AddListener(() => UIManager.Instance.ShowPanel<SettingPanel>("SettingPanel"));
            saveBtn.onClick.AddListener(() => SaveManager.Save());
        }

        protected override void OnClick(string btnName)
        {
            base.OnClick(btnName);
            switch (btnName)
            {
                case "SettingBtn":
                    AkSoundEngine.PostEvent("Menu_enter", gameObject);
                    break;
                case "SaveBtn":
                    AkSoundEngine.PostEvent("Menu_confirm", gameObject);
                    break;
            }
        }
    }
}
