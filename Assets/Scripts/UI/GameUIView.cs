using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerNameText;
        [SerializeField] private TMP_InputField  _winningNumberInputField;
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
            _spinButton             .gameObject.SetActive(false);
            _shopButton             .gameObject.SetActive(false);
            _winningNumberInputField.gameObject.SetActive(false);
        }
        
        private void EnableButtons()
        {
            _spinButton             .gameObject.SetActive(true);
            _shopButton             .gameObject.SetActive(true);
            _winningNumberInputField.gameObject.SetActive(true);
        }
        
        public void SetPlayerName(string playerName)
        {
            _playerNameText.text = playerName;
        }
        
        public int GetWinningNumber()
        {
            if (_winningNumberInputField.text == string.Empty) return -1;
            
            int winningNumber = int.Parse(_winningNumberInputField.text);

            if (winningNumber < 0 || winningNumber > 36)
            {
                return -1;
            }
            
            return winningNumber;
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