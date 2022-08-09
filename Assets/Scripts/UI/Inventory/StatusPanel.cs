using Base.Event;
using Data;
using Save;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class StatusPanel : BasePanel
    {
        [SerializeField] private InventoryDataSO _data;
        [SerializeField] private int _maxBatteryNum;
        [SerializeField] private int _maxPower;

        private Button _key1, _key2;
        private TextMeshProUGUI _batteryNum;
        private Image _powerRemaining;

        protected override void Awake()
        {
            base.Awake();
            Init();
            _data = Instantiate(_data);
            
            // 读档
            var data = SaveManager.GetValue(this.GetInstanceID().ToString());
            if (!string.IsNullOrEmpty(data)) 
                JsonUtility.FromJsonOverwrite(data, _data);
        }

        private void Update()
        {
            RefreshUI();
        }

        private void OnDestroy()
        {
            EventCenter.Instance.RemoveEventListener("UseBattery", UseBattery);
            EventCenter.Instance.RemoveFuncListener<int, bool>("PickUpBattery", PickUpBattery);
            EventCenter.Instance.RemoveFuncListener<float, bool>("UseBatteryPower", UseBatteryPower);
            
            // 存档
            var data = JsonUtility.ToJson(_data);
            SaveManager.Register(this.GetInstanceID().ToString(), data);
        }
        
        private void Init()
        {
            _batteryNum = GetControl<TextMeshProUGUI>("Number");
            _powerRemaining = GetControl<Image>("Remaining");
            _key1 = GetControl<Button>("Key1");
            _key2 = GetControl<Button>("Key2");
            
            EventCenter.Instance.AddEventListener("UseBattery", UseBattery);
            EventCenter.Instance.AddFuncListener<int, bool>("PickUpBattery", PickUpBattery);
            EventCenter.Instance.AddFuncListener<float, bool>("UseBatteryPower", UseBatteryPower);
        }

        private void RefreshUI()
        {
            _batteryNum.text = _data.batteryNum + " / " + _maxBatteryNum;
            _powerRemaining.fillAmount = _data.powerRemaining / _maxPower;

            _key1.gameObject.SetActive(_data.hasKey1);
            _key2.gameObject.SetActive(_data.hasKey2);
        }

        private bool PickUpBattery(int num = 1)
        {
            if (_data.batteryNum >= _maxBatteryNum)
                return false;

            _data.batteryNum += num;
            return true;
        }

        private void UseBattery()
        {
            if (_data.batteryNum <= 0) return;
            _data.batteryNum--;
            _data.powerRemaining = _maxPower;
        }

        private bool UseBatteryPower(float speed)
        {
            _data.powerRemaining -= speed * Time.deltaTime;

            return _data.powerRemaining > 0;
        }

        public void OnKeyClick(ItemDataSO keyData)
        {
            UIManager.Instance.ShowPanel<ItemInfoPanel>("ItemInfoPanel", 
                callBack: panel => panel.UpdateInfo(keyData));
        }
    }
}
