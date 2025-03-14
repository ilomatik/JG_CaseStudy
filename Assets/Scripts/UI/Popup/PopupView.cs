using System;
using System.Collections;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public abstract class PopupView : MonoBehaviour
    {
        [SerializeField] private Button          _closeButton;
        [SerializeField] private Image           _background;
        [SerializeField] private RectTransform   _panel;
        [SerializeField] private GameObject      _infoTextPanel;
        [SerializeField] private TextMeshProUGUI _infoText;
        
        private Vector2 _hiddenPosition;
        private Vector2 _visiblePosition;
        private float   _animationDuration;
        private Color   _startColor;
        private Color   _endColor;

        public void Initialize(float animationDuration)
        {
            _animationDuration = animationDuration;
            _hiddenPosition    = _panel.anchoredPosition;
            _visiblePosition   = Vector2.zero;
            
            _startColor = new Color(0, 0, 0, 0);
            _endColor   = new Color(0, 0, 0, 0.5f);
            
            _closeButton.onClick.AddListener(Close);
            
            gameObject.SetActive(false);
        }
        
        public void Open(PopupInfo info = null)
        {
            gameObject.SetActive(true);
            
            if (info != null)
            {
                _infoTextPanel.SetActive(true);
                _infoText.text = info.Text;
            }
            
            GameEvents.PopupOpen();
            StartCoroutine(AnimatePopup(_hiddenPosition, 
                                               _visiblePosition, 
                                               _startColor, 
                                               _endColor));
        }
        
        public void Close()
        {
            AudioEvents.PlayButtonClick();
            
            StartCoroutine(AnimatePopup(_visiblePosition, 
                                               _hiddenPosition, 
                                               _endColor, 
                                               _startColor, 
                                               () =>
                                               {
                                                   GameEvents.PopupClose();
                                                   Destroy(gameObject);
                                               }));
        }
        
        private IEnumerator AnimatePopup(Vector2 from, 
                                         Vector2 to, 
                                         Color fromColor,
                                         Color toColor, 
                                         Action onComplete = null)
        {
            float elapsedTime = 0f;

            while (elapsedTime < _animationDuration)
            {
                _panel.anchoredPosition = Vector2.Lerp(from, to, elapsedTime / _animationDuration);
                _background.color       = Color.Lerp(fromColor, toColor, elapsedTime / _animationDuration);
                
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            _panel.anchoredPosition = to;
            _background.color       = toColor;
            onComplete?.Invoke();
            
        }
    }
}