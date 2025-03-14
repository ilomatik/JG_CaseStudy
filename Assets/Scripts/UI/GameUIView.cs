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
        [SerializeField] private Button          _shopButton;
        
        private void Start()
        {
            _spinButton.onClick.AddListener(OnSpinButtonClicked);
            _shopButton.onClick.AddListener(GameEvents.ClickShopButton);
        }
        
        private void OnEnable()
        {
            GameEvents.OnWheelStartSpin += SetSpinButtonUnclickable;
            GameEvents.OnWheelStopSpin  += SetSpinButtonClickable;
            GameEvents.OnPopupOpen      += DisableButtons;
            GameEvents.OnPopupClose     += EnableButtons;
        }
        
        private void OnDisable()
        {
            GameEvents.OnWheelStartSpin -= SetSpinButtonUnclickable;
            GameEvents.OnWheelStopSpin  -= SetSpinButtonClickable;
            GameEvents.OnPopupOpen      -= DisableButtons;
            GameEvents.OnPopupClose     -= EnableButtons;
        }
        
        private void DisableButtons()
        {
            _spinButton.gameObject.SetActive(false);
            _shopButton.gameObject.SetActive(false);
        }
        
        private void EnableButtons()
        {
            _spinButton.gameObject.SetActive(true);
            _shopButton.gameObject.SetActive(true);
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
    }
}