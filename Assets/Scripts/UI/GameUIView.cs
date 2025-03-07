using System;
using System.Linq;
using Enums;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerNameText;
        [SerializeField] private Button          _spinButton;
        [SerializeField] private TMP_Dropdown    _dropdown;
        [SerializeField] private TMP_InputField  _chipInputField;
        [SerializeField] private Button          _addChipButton;
        
        private void Start()
        {
            _spinButton   .onClick.AddListener(OnSpinButtonClicked);
            _addChipButton.onClick.AddListener(OnAddChipButtonClicked);
            
            SetDropdownOptions();
        }
        
        private void SetDropdownOptions()
        {
            _dropdown.ClearOptions();
            var enumValues = Enum.GetValues(typeof(ChipType)).Cast<ChipType>().Select(e => e.ToString()).ToList();
            _dropdown.AddOptions(enumValues);
        }
        
        private void OnEnable()
        {
            GameEvents.OnWheelStartSpin += SetSpinButtonUnclickable;
            GameEvents.OnWheelStopSpin  += SetSpinButtonClickable;
        }
        
        private void OnDisable()
        {
            GameEvents.OnWheelStartSpin -= SetSpinButtonUnclickable;
            GameEvents.OnWheelStopSpin  -= SetSpinButtonClickable;
        }
        
        public void SetPlayerName(string playerName)
        {
            _playerNameText.text = playerName;
        }
        
        private void SetSpinButtonClickable()
        {
            _spinButton.interactable = true;
        }
        
        private void SetSpinButtonUnclickable()
        {
            _spinButton.interactable = false;
        }
        
        private void OnSpinButtonClicked()
        {
            GameEvents.ClickSpinButton();
        }
        
        private void OnAddChipButtonClicked()
        {
            string chipType   = _dropdown.options[_dropdown.value].text;
            string chipAmount = _chipInputField.text;
            
            GameEvents.ChangeChipAmount(chipType, int.Parse(chipAmount));
        }
    }
}