using System;
using System.Linq;
using Enums;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public class ShopPopup : PopupView
    {
        [SerializeField] private Button          _buyButton;
        [SerializeField] private TMP_Dropdown    _dropdown;
        [SerializeField] private TMP_InputField  _chipInputField;
        
        private void Start()
        {
            _buyButton  .onClick.AddListener(Buy);
            
            SetDropdownOptions();
        }
        
        private void SetDropdownOptions()
        {
            _dropdown.ClearOptions();
            var enumValues = Enum.GetValues(typeof(ChipType)).Cast<ChipType>().Select(e => e.ToString()).ToList();
            _dropdown.AddOptions(enumValues);
        }
        
        private void Buy()
        {
            string chipType   = _dropdown.options[_dropdown.value].text;
            string chipAmount = _chipInputField.text;
            
            GameEvents.ChangeChipAmount(chipType, int.Parse(chipAmount));
        }
    }
}