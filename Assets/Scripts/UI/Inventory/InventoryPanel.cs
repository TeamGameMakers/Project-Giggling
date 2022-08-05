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
        }

        private void Update()
        {
            RefreshUI();
        }

        private void RefreshUI()
        {
            _batteryNum.text = _data.batteryNum + " / " + _maxBatteryNum;
            _powerRemaining.fillAmount = _data.powerRemaining / _maxPower;

            _key1.gameObject.SetActive(_data.hasKey1);
            _key2.gameObject.SetActive(_data.hasKey2);
        }

        public void UseBattery()
        {
            if (_data.batteryNum <= 0) return;
            _data.batteryNum--;
            _data.powerRemaining = _maxPower;
        }

        public bool UseBatteryPower(float speed)
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
