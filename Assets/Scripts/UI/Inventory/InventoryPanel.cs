using Base.Event;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class InventoryPanel : BasePanel
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

        private void Update()
        {
            RefreshUI();
        }

        private void OnDestroy()
        {
            EventCenter.Instance.RemoveEventListener("UseBattery", UseBattery);
            EventCenter.Instance.RemoveFuncListener<int, bool>("PickUpBattery", PickUpBattery);
            EventCenter.Instance.RemoveFuncListener<float, bool>("UseBatteryPower", UseBatteryPower);
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
