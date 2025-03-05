using Events;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameUIView : MonoBehaviour
    {
        [SerializeField] private Button _spinButton;
        
        private void Start()
        {
            _spinButton.onClick.AddListener(OnSpinButtonClicked);
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
            // Spin the wheel
            GameEvents.ClickSpinButton();
        }
    }
}