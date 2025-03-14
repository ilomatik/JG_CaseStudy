using System.Collections.Generic;
using UI.Popup;
using UnityEngine;

namespace Managers
{
    public class PopupManager : MonoBehaviour, IPopupManager
    {
        [SerializeField] private float           _popupAnimationDuration;
        [SerializeField] private Transform       _popupParent;
        [SerializeField] private List<PopupView> _popupViews;

        private Dictionary<string, GameObject> _popupsByName;
        private PopupView                      _currentPopup;

        public void Initialize()
        {
            _popupsByName = new Dictionary<string, GameObject>();

            foreach (PopupView popupView in _popupViews)
            {
                popupView.Initialize(_popupAnimationDuration);
                _popupsByName.Add(popupView.name, popupView.gameObject);
            }
        }

        public void ShowPopup(string popupName, PopupInfo info = null)
        {
            if (_currentPopup != null) return;
            
            GameObject popupPrefab   = _popupsByName[popupName];
            GameObject popupInstance = Instantiate(popupPrefab, _popupParent);
            PopupView  popupView     = popupInstance.GetComponent<PopupView>();
            
            popupInstance.name = popupName;
            popupView.Initialize(_popupAnimationDuration);
            popupView.Open(info);
            
            _currentPopup = popupView;
        }

        public void HidePopup(string popupName)
        {
            _currentPopup.Close();
        }
    }
}